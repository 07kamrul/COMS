using AutoMapper;
using Core.Repository;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IRoleRepository _roleRepository;
        IMapper _mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(_userRepository.GetById(id));
        }

        public User GetbyEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public List<RoleResponse> GetRoles()
        {
            return _mapper.Map<List<RoleResponse>>(_roleRepository.GetAll());
        }

        public RoleResponse SaveRole(RoleRequestModel role)
        {
            Role saveRole = _roleRepository.Add(_mapper.Map<Role>(role));
            return _mapper.Map<RoleResponse>(saveRole);
        }

        public void UpdateRole(RoleRequestModel role)
        {
            Role saveRole = _roleRepository.GetById(role.Id);
            saveRole.Name = role.Name;
            _roleRepository.Update(saveRole);
        }

        public Page<UserResponse> GetUserList(string userSearchText, int skip, int take)
        {
            var query = _userRepository.GetAll().Where(x => string.IsNullOrEmpty(userSearchText)
            || (x.Email == userSearchText
            || x.Phone == userSearchText
            || x.FirstName.Contains(userSearchText)
            || x.LastName.Contains(userSearchText)));

            return new Page<UserResponse>
            {
                Data = _mapper.Map<List<UserResponse>>(query.Skip(skip).Take(take).ToList()),
                Total = query.Count()
            };
        }

        public bool IsExistingUser(string email)
        {
            return _userRepository.IsExistingUser(email);
        }

        public UserResponse SaveUser(UserRequestModel user)
        {
            User saveUser = _userRepository.Add(_mapper.Map<User>(user));
            return _mapper.Map<UserResponse>(saveUser);
        }

        public void UpdateRefreshToken(int userId, string refreshToken)
        {
            _userRepository.UpdateRefreshToken(userId, refreshToken);
        }

        public void UpdateUser(UserRequestModel user)
        {
            User saveUser = _userRepository.GetById(user.Id);
            saveUser.FirstName = user.FirstName;
            saveUser.LastName = user.LastName;
            saveUser.Email = user.Email;
            saveUser.Phone = user.Phone;
            saveUser.Password = string.IsNullOrEmpty(user.Password) ? saveUser.Password : user.Password;
            saveUser.Roles = _mapper.Map<List<Role>>(user.Roles);
            
            _userRepository.Update(saveUser);
        }

        public UserResponse GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserResponse>(user);
        }

        public List<UserResponse> GetAllActiveUser()
        {
            var activeUsers = _userRepository.GetAll().Where(x => x.IsActive);
            return _mapper.Map<List<UserResponse>>(activeUsers);
        }

        public List<UserResponse> GetInActiveUsers()
        {
            var inActiveUsers = _userRepository.GetAll().Where(x => !x.IsActive);
            return _mapper.Map<List<UserResponse>>(inActiveUsers);
        }

        public List<UserResponse> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<List<UserResponse>>(users);
        }
    }
}
