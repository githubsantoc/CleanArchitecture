
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
using Application.common.Authentication;

namespace Application.Command.UsersCommand.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, string>
    {

        private readonly IUserWrapper _userManager;
        private readonly IJwtToken _jwtToken;
        private readonly JWTandRefreshService _jwtAndRefreshService;

        public RefreshTokenHandler(JWTandRefreshService jwtAndRefreshService, IUserWrapper userManager, IJwtToken jwtToken)
        {
            _jwtAndRefreshService = jwtAndRefreshService;
            _userManager = userManager;
            _jwtToken = jwtToken;
        }
        public async Task<string> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            if (request.RefreshToken == null)
                throw new Exception("Please provide refresh token");

            if (_jwtAndRefreshService.RefreshToken != request.RefreshToken)
                throw new Exception("Invalid refresh token");

            var principal = _jwtToken.GetPrincipalFromExpiredToken(_jwtAndRefreshService.JwtToken, _jwtAndRefreshService.TokenExpiry);
            
            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsy(username);


            var jwtToken = _jwtToken.GenerateToken(principal.Claims.ToList());
            var refreshToken = _jwtToken.GenerateRefreshToken();


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
    }
}

