using Core.Common;
using Core.Repository;
using Core.RequestModels;
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

        public Page<Members> Search(MemberSearchRequestModel searchModel, int skip, int take)
        {
            
        }
    }
}
