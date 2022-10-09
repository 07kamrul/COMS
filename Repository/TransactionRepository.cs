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
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {

        MCLDBContext _context;
        public TransactionRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }


        public List<Transaction> GetTransactionsByMemberId(int memberId)
        {
            return _context.Transactions.AsNoTracking()
                .Where(x => x.MemberId == memberId).ToList();
        }

        public List<Transaction> GetTransactionsByAccountId(int accountId)
        {
            return _context.Transactions.AsNoTracking()
                .Where(x => x.AccountId == accountId).ToList();
        }

            public List<Transaction> GetTransactionsByProject(int projectId)
        {
            return _context.Transactions.AsNoTracking()
                .Where(x => x.ProjectId == projectId).ToList();
        }

        public bool IsExistingTransaction(int memberId, DateTime transactionDate, int transactionType)
        {
            return _context.Transactions.AsNoTracking()
            .Where(x => x.MemberId == memberId 
            || x.TransactionDate == transactionDate 
            || x.TransactionType == transactionType)
            .Count() > 0;
        }


    }
}
