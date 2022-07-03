using Core.Service;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace COMS.Security
{
    public class PopulateClaimsMiddleware : IMiddleware
    {

        private readonly IUserService _userService;
        public PopulateClaimsMiddleware(IUserService userService)
        {
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(context.User != null && context.User.HasClaim(c => c.Type == ClaimTypes.Email) == true)
            {
                var emailClaim = context.User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault();

                var email = emailClaim.Value;
                var user = _userService.GetbyEmail(email);
                if(user != null)
                {
                    var claims = new List<Claim>();
                    claims.AddRange(context.User.Claims);
                    var identity = new ClaimsIdentity(claims, "COMS");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
            await next(context);
        }

        private static void AddClaim(List<Claim> claims, string claimTypeName, string value)
        {
            claims.Add(new Claim(claimTypeName, value));
        }
    }
}
