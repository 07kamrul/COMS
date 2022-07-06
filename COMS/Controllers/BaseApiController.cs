using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace COMS.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected string GetLoggedInUserEmail()
        {
            return this.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        protected int GetLoggedInId()
        {
            return Convert.ToInt32(this.User.FindFirst(ClaimTypes.Name).Value);
        }
    }
}
