using Core.RequestModels;
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
        void UpdateUser(UserRequest user);
        UserResponse SaveUser(UserRequest user);
        void UpdateRefreshToken(int userId, string refreshToken);
        Page<UserResponse> GetUserList(string userSearchText, int skip, int take);
        void DeleteUser(int id);
        List<RoleResponse> GetRoles();
        RoleResponse SaveRole(RoleRequest role);
        void UpdateRole(RoleRequest role);
        UserResponse GetUser(int id);
        List<UserResponse> GetAllActiveUser();
        List<UserResponse> GetInActiveUsers();
        List<UserResponse> GetAllUsers();
    }
}
