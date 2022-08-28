using Core.RequestModels;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface ITransactionService
    {
        List<TransactionResponse> GetTransactions();
        TransactionResponse GetTransaction(int id);
        List<TransactionResponse> GetVerifiedTransactions();
        List<TransactionResponse> GetRequestVerifyTransactions();
        bool IsExistingTransaction(int memberId, DateTime transactionDate, int transactionType);
        TransactionResponse SaveTransaction(TransactionRequest transaction);
        List<TransactionResponse> GetTransactionsByMemberId(int memberId);
        List<TransactionResponse> GetTransactionsByProject(int projectId);
        void DeleteTransaction(int id);
    }
}
