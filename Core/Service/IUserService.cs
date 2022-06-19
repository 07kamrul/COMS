using Core.RequestModels;
using Core.ViewModel;
using Core.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IUserService
    {
        bool IsExistingUser(string email);
        User GetbyEmail(string email);
        void UpdateUser(UserRequestModel user);
        UserResponse SaveUser(UserRequestModel user);
        void UpdateRefreshToken(int userId, string refreshToken);
        Page<UserResponse> GetUserList(string userSearchText, int skip, int take);
        void DeleteUser(int id);
        List<RoleResponse> GetRoles();

    }
}
