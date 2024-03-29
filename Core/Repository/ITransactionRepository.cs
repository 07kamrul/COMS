﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        bool IsExistingTransaction(int memberId, DateTime transactionDate, int transactionType);
        List<Transaction> GetTransactionsByMemberId(int memberId);
        List<Transaction> GetTransactionsByAccountId(int accountId);
        List<Transaction> GetTransactionsByProject(int projectId);
    }
}
