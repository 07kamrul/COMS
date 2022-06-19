using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool IsExistingUser(string email);
        UserViewModel GetUserByEmail(string email);
        void UpdateRefreshToken(int userId, string refreshToken);
    }
}
