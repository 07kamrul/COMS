using Core.Common;
using Core.Repository;
using Core.RequestModels;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DepositRepository : BaseRepository<Deposites>, IDepositRepository
    {
        MCLDBContext _context;

        public DepositRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }

        public Page<Deposites> Search(DepositSearchRequestModel searchModel, int skip, int take)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            IQueryable<Deposites> query = _context.Set<Deposites>();

            query = query.Where(x => (searchModel.MemberId == 0 || (x.Members.Email.Length > 0 && x.Members.Id == searchModel.MemberId))
                && (searchModel.)
            );
        }
    }
}
