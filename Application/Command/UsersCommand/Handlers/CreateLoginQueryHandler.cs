using Application.services;
using Application.Wrapper;
using Domains.Entities;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Application.Command.UsersCommand;

namespace Application.Command.UsersCommand.Handlers
{
    public class CreateLoginQueryHandler : IRequestHandler<CreateLoginQuery, string>
    {
        private readonly IUserWrapper _userManager;
        private readonly IConfiguration _configuration;
        private readonly JWTandRefreshService _jwtAndRefreshService;
        private readonly IMailServices _iMailServices;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public CreateLoginQueryHandler(IMailServices iMailServices, JWTandRefreshService jwtAndRefreshService, IUserWrapper userManager, IConfiguration configuration, IBackgroundJobClient backgroundJobClient)
        {
            _iMailServices = iMailServices;
            _jwtAndRefreshService = jwtAndRefreshService;
            _userManager = userManager;
            _configuration = configuration;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<string> Handle(CreateLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAs(request.Email.ToUpperInvariant());
            if (user != null && await _userManager.CheckPWAsync(user, request.Password))
            {
                //assigning roles to the user and claim them
                var roles = await _userManager.GetRolesAs(user);
                var claims = new List<Claim>();
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Name, request.Email));
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                //HAngfire concept
                if (user.LoginDateTime == null || (DateTime.Now - user.LoginDateTime).TotalMinutes > 1)
                {
                    user.LoginDateTime = DateTime.UtcNow;
                    await _userManager.UpdateAs(user);
                    _backgroundJobClient.Enqueue(() => _iMailServices.SendEmailAsync("test@gmail.com", "Login", "You are logged in"));
                    //   BackgroundJob.Schedule(() => _iMailServices.SendEmailAsync("santoshibanj100@gmail.com", "Remainder", "You are logged in since 5 min. Do you want to log out?"), TimeSpan.FromMinutes(5));

                }
                //part for token generation
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(5),
                    signingCredentials: signIn);

                //added for refresh token
                var refreshToken = GenerateRefreshToken();

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token).ToString();
                var token1 = new
                {
                    refresh_token = refreshToken.Token,
                    jwt_token = jwtToken
                };

                _jwtAndRefreshService.JwtToken = jwtToken;
                _jwtAndRefreshService.TokenExpiry = refreshToken.Expired;
                _jwtAndRefreshService.RefreshToken = refreshToken.Token;

                return JsonConvert.SerializeObject(token1);

                // return new JwtSecurityTokenHandler().WriteToken(token);
            }

            // return user;
            throw new Exception("Provide Valid username or password");
        }
        private static RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expired = DateTime.Now.AddMinutes(2)
            };
            return refreshToken;
        }
    }
}

