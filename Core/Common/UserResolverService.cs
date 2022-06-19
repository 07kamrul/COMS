using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public interface IUserResolverService
    {
        HttpContext GetContext();
        string GetUser();
    }
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public HttpContext GetContext()
        {
            return _context.HttpContext;
        }

        public string GetUser()
        {
            return _context.HttpContext.User?.Identity?.Name;
        }
    }
}
