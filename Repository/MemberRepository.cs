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
    public class MemberRepository : BaseRepository<Members>, IMemberRepository
    {
        MCLDBContext _context;
        public MemberRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }
    }
}
