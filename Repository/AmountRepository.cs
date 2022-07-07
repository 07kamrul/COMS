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
    public class AmountRepository : BaseRepository<Amounts>, IAmountRepository
    {
        MCLDBContext _context;
        public AmountRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }

        public Page<Amounts> Search(AmountSearchRequestModel amountSearchRequestModel, int skip, int take)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            IQueryable<Amounts> query = _context.Set<Amounts>();

            query = query.Where(x => (amountSearchRequestModel.MemberId == 0 || (x.Members.Email.Length > 0 && x.Members.Id == amountSearchRequestModel.MemberId))
                && (string.IsNullOrEmpty(amountSearchRequestModel.MemberName) || x.Members.Name.Contains(amountSearchRequestModel.MemberName))
                && (amountSearchRequestModel.Amount == 0 || x.Amount == amountSearchRequestModel.Amount)
                && (amountSearchRequestModel.AmountDate == null || x.AmountDate == amountSearchRequestModel.AmountDate)
                && x.IsActive
                && x.IsVerified
            );

            return new Page<Amounts>
            {
                Data = query.OrderBy(a => a.Id).Skip(skip).Take(take)
                    .Include(x => x.Members).ThenInclude(x => x.Email)
                    .Include(x => x.Members).ThenInclude(x => x.Name)
                    .Include(x => x.Amount)
                    .Include(x => x.AmountDate)
                    .Include(x => x.Deposites).ThenInclude(x => x.DepositeDate)
                    .Include(x => x.IsVerified).ToList(),
                Total = query.Count()
            };
        }
    }
}
