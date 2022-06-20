using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims, DateTime expirationTime);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
