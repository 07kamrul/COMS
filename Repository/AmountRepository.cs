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
    public class AmountRepository : BaseRepository<Amount>, IAmountRepository
    {
        MCLDBContext _context;
        public AmountRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }

        public Page<Amount> Search(AmountSearchRequestModel amountSearchRequestModel, int skip, int take)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            IQueryable<Amount> query = _context.Set<Amount>();

            query = query.Where(x => (amountSearchRequestModel.MemberId == 0 || (x.Member.Email.Length > 0 && x.Member.Id == amountSearchRequestModel.MemberId))
                && (string.IsNullOrEmpty(amountSearchRequestModel.MemberName) || x.Member.Name.Contains(amountSearchRequestModel.MemberName))
                && (amountSearchRequestModel.Amount == 0 || x.Amounts == amountSearchRequestModel.Amount)
                && (amountSearchRequestModel.AmountDate == null || x.AmountDate == amountSearchRequestModel.AmountDate)
                && x.IsActive
                && x.IsVerified
            );

            return new Page<Amount>
            {
                Data = query.OrderBy(a => a.Id).Skip(skip).Take(take)
                    .Include(x => x.Member).ThenInclude(x => x.Email)
                    .Include(x => x.Member).ThenInclude(x => x.Name)
                    .Include(x => x.Amounts)
                    .Include(x => x.AmountDate)
                    .Include(x => x.IsVerified).ToList(),
                Total = query.Count()
            };
        }
    }
}
