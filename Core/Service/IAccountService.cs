using Core.RequestModels;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IAccountService
    {
        AccountResponse GetAccount(int id);
        List<AccountResponse> GetAccounts();
        List<AccountResponse> GetInactiveAccounts();
        List<AccountResponse> GetVerifiedAccounts();
        List<AccountResponse> GetAccountsByMember(int id);
        List<AccountResponse> GetRequestVerifyAccounts();
        AccountResponse SaveAccount(AccountRequest account);
        void DeleteAccount(int id);
        List<AccountResponse> GetAccountsByProject(int id);
    }
}
