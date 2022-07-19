using Core.Common;
using Core.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepository : BaseRepository<Roles>, IRoleRepository
    {
        public RoleRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {

        }
    }
}
