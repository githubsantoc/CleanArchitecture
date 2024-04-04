
using CQRSApplication.UserInfo.Query;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Wrapper;
using Microsoft.Extensions.Configuration;
using Application.services;

namespace Application.Command.UsersCommand.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, string>
    {

        private readonly IUserWrapper _userManager;
        private readonly IConfiguration _configuration;
        private readonly JWTandRefreshService _jwtAndRefreshService;

        public RefreshTokenHandler(JWTandRefreshService jwtAndRefreshService, IUserWrapper userManager, IConfiguration configuration)
        {
            //  _context = context;
            _jwtAndRefreshService = jwtAndRefreshService;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            if (request.RefreshToken == null)
                throw new Exception("Please provide refresh token");

            if (_jwtAndRefreshService.RefreshToken != request.RefreshToken)
                throw new Exception("Invalid refresh token");

            //var principal = GetPrincipalFromExpiredToken(_jwtAndRefreshService.Jwt_refreshExp);
            var principal = GetPrincipalFromExpiredToken(_jwtAndRefreshService.JwtToken, _jwtAndRefreshService.TokenExpiry);
            /*  if (principal == null)
              {
                  throw new Exception("Invalid access token");
              }*/
            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsy(username);


            var jwtToken = CreateToken(principal.Claims.ToList());
            var refreshToken = GenerateRefreshToken();


            var token1 = new
            {
                refresh_token = refreshToken.Token,
                jwt_token = jwtToken
            };
            _jwtAndRefreshService.JwtToken = jwtToken;
            _jwtAndRefreshService.TokenExpiry = refreshToken.Expired;
            _jwtAndRefreshService.RefreshToken = refreshToken.Token;

            return JsonConvert.SerializeObject(token1);

        }
        private string CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token).ToString();
            return jwtToken;
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
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string s, DateTime d)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(s, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
            /*if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }*/
            if (DateTime.Now > d)
                throw new Exception("Refresh Token Expired!");
            return principal;
        }
    }
}

