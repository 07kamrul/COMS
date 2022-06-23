using Core.Common;
using Core.Repository;
using Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private MCLDBContext _context;
        private IUserResolverService _user;

        public UserRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
            _user = user;
        }

        public override User GetById(int id)
        {
            return _context.Users.AsNoTracking().Include(x => x.Roles).Where(x => x.Id == id).FirstOrDefault();
        }

        public override IQueryable<User> GetAll()
        {
            return _context.Users.AsNoTracking().Include(x => x.Roles).Where(x => x.IsActive).AsQueryable();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.AsNoTracking().Include(x => x.Roles).Where(x => x.Email.ToLower() == email.ToLower() && x.IsActive)?.FirstOrDefault();
        }

        public bool IsExistingUser(string email)
        {
            return _context.Users.AsNoTracking().Where(x => x.Email.ToLower() == email.ToLower() && x.IsActive).Count() > 0;
        }

        public void UpdateRefreshToken(int userId, string refreshToken)
        {
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            user.RefreshToken = refreshToken;
            base.Update(user);
        }

        public override User Update(User entity)
        {
            if(entity.Roles != null && entity.Roles.Count > 0)
            {
                List<Role> roles = entity.Roles.ToList();
                var rolesRemove = _context.RoleUser.Where(x => x.UserId == entity.Id);
                _context.RoleUser.RemoveRange(rolesRemove);
                _context.SaveChanges(_user.GetUser());
                entity.Roles = roles;
            }

            entity = _context.Users.Update(entity).Entity;
            entity.ModificationDate = DateTime.UtcNow;
            int userId;
            int.TryParse(_user.GetUser(), out userId);
            entity.ModifiedBy = (userId == 0 ? null : userId);
            _context.SaveChanges(_user.GetUser());

            return entity;
        }
    }
}
