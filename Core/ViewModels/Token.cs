using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class Token
    {
        public class TokenRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public bool Remember { get; set; }
        }

        public class RefreshTokenRequest
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
