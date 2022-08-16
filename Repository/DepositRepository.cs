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
    public class DepositRepository : BaseRepository<Deposite>, IDepositRepository
    {
        MCLDBContext _context;

        public DepositRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }

        public Page<Deposite> Search(DepositSearchRequestModel searchModel, int skip, int take)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            IQueryable<Deposite> query = _context.Set<Deposite>();

            query = query.Where(x => (searchModel.MemberId == 0 || (x.Member.Email.Length > 0 && x.Member.Id == searchModel.MemberId))
                && (string.IsNullOrEmpty(searchModel.MemberName) || x.Member.Name.Contains(searchModel.MemberName))
                && (searchModel.Amount == 0 )
                && (searchModel.DepositeDate == null || x.DepositeDate == searchModel.DepositeDate)
                && x.IsActive
                && x.IsVerified
            );

            return new Page<Deposite>
            {
                Data = query.OrderBy(a => a.Id).Skip(skip).Take(take)
                    .Include(x => x.Member).ThenInclude(x => x.Email)
                    .Include(x => x.Attachments).ThenInclude(x => x.AttachmentType)
                    .Include(x => x.DepositeDate)
                    .Include(x => x.Member).ThenInclude(x => x.IsActive).ToList(),
                Total = query.Count()
            };
        }
    }
}
