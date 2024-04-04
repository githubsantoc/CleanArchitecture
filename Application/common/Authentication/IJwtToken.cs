using Application.Command.UsersCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Authentication
{
    public interface IJwtToken
    {
        string GenerateToken(List<Claim> claims);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string s, DateTime d);
        RefreshToken GenerateRefreshToken();
    }
}
