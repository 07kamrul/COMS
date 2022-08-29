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

        public List<Account> GetAccountsByProject(int id)
        {
            return _context.Accounts.AsNoTracking().Where(x => x.ProjectId == id).ToList();
        }

        public List<Account> GetInactiveAccounts()
        {
            return _context.Accounts.AsNoTracking().Where(x => !x.IsActive).ToList();
        }

        public List<Account> GetRequestVerifyAccounts()
        {
            return _context.Accounts.AsNoTracking().Where(x => !x.IsVerified).ToList();
        }

        public List<Account> GetVerifiedAccounts()
        {
            return _context.Accounts.AsNoTracking().Where(x => x.IsVerified).ToList();
        }
    }
}
