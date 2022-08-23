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
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {

        MCLDBContext _context;
        public ProjectRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }
    }
}
