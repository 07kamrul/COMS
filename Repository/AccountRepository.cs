using Core.Common;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        MCLDBContext _context;
        public AccountRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }

        public List<Account> GetAccountsByMember(int id)
        {
            return _context.Accounts.AsNoTracking().Where(x => x.MemberId == id).ToList();
        }
    }
}
