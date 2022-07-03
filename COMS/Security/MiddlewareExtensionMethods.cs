using Microsoft.AspNetCore.Builder;

namespace COMS.Security
{
    public static class MiddlewareExtensionMethods
    {
        public static IApplicationBuilder UserPopulateClaimsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PopulateClaimsMiddleware>();
        }
    }
}
