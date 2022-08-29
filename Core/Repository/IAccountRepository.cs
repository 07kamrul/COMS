using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        List<Account> GetAccountsByMember(int id);
        List<Account> GetInactiveAccounts();
        List<Account> GetRequestVerifyAccounts();
        List<Account> GetVerifiedAccounts();
        List<Account> GetAccountsByProject(int id);
    }
}
