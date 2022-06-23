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
    public class AmountRepository : BaseRepository<Amounts>, IAmountRepository
    {
        MCLDBContext _context;
        public AmountRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }
    }
}
