using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        bool IsExistingUser(string email);
        Users GetUserByEmail(string email);
        void UpdateRefreshToken(int userId, string refreshToken);
    }
}
