using Core.RequestModels;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Core.Common.Enums;

namespace COMS.Security
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(params PermissionType[] claimValues) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(claimValues))};
        }

        public class ClaimRequirementFilter : IAuthorizationFilter
        {
            private readonly Claim _claim;

            public ClaimRequirementFilter(Claim claim)
            {
                _claim = claim;
            }

            public async void OnAuthorization(AuthorizationFilterContext context)
            {
                if (context.RouteData.Values["controller"].ToString() == "User" && context.RouteData.Values["action"].ToString() == "UpdateUser")
                {
                    var userId = context.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;

                    var reader = new StreamReader(context.HttpContext.Request.Body);
                    var content = await reader.ReadToEndAsync();
                    context.HttpContext.Request.Body = new MemoryStream(Encoding.ASCII.GetBytes(content));
                    var user = JsonConvert.DeserializeObject<UserRequestModel>(content);

                    if(user.Id.ToString() == userId)
                    {
                        return;
                    }
                }

                var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type);
                if (hasClaim)
                {
                    List<RoleResponse> roles = JsonConvert.DeserializeObject<List<RoleResponse>>(context.HttpContext.User.Claims.Where(x => x.Type == _claim.Type).FirstOrDefault().Value);
                    List<int> claimRoleIds = JsonConvert.DeserializeObject<List<int>>(_claim.Value);
                    hasClaim = roles.Select(x => x.Id).Intersect(claimRoleIds).Any();
                }
                if (!hasClaim)
                {
                    context.Result = new ObjectResult("You are not authorized to do this operation") { StatusCode = 403 };
                }
            }
        }
    }
}
