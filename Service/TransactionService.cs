using AutoMapper;
using AutoMapper.Execution;
using Core;
using Core.Common;
using Core.FileStore;
using Core.Repository;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Member = Model.Member;
using Transaction = Model.Transaction;

namespace Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IFileStore _fileStore;
        private readonly IConfiguration _configuration;
        private readonly IProjectService _projectService;
        private readonly IAccountService _accountService;
        private readonly IMemberService _memberService;
        private readonly MCLDBContext _context;

        public TransactionService(IMapper mapper, 
            ITransactionRepository transactionRepository,
            IAttachmentRepository attachmentRepository,
            IAccountRepository accountRepository,
            IMemberRepository memberRepository,
            IFileStore fileStore,
            IConfiguration configuration,
            IProjectService projectService,
            IAccountService accountService,
            IMemberService memberService,
            MCLDBContext context)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _attachmentRepository = attachmentRepository;
            _accountRepository = accountRepository;
            _memberRepository = memberRepository;
            _fileStore = fileStore;
            _configuration = configuration;
            _projectService = projectService;
            _accountService = accountService;
            _memberService = memberService;
            _context = context;
        }


        public List<TransactionResponse> GetRequestVerifyTransactions()
        {
            return _mapper.Map<List<TransactionResponse>>(_transactionRepository
                .GetAll().Where(x => x.IsActive && !x.IsVerified));
        }


        public TransactionResponse GetTransaction(int id)
        {
            return _mapper.Map<TransactionResponse>(_transactionRepository.GetById(id));
        }


        public List<TransactionResponse> GetTransactions()
        {
            var getAll = _transactionRepository.GetAll();
            return _mapper.Map<List<TransactionResponse>>(getAll);
        }

        public List<TransactionResponse> GetTransactionsByMemberId(int memberId)
        {
            List<Transaction> memberTransaction = _transactionRepository
                .GetTransactionsByMemberId(memberId);

            return _mapper.Map<List<TransactionResponse>>(memberTransaction);
        }

        public List<TransactionResponse> GetTransactionsByProject(int projectId)
        {
            List<Transaction> projectTransaction = _transactionRepository
                .GetTransactionsByProject(projectId);

            return _mapper.Map<List<TransactionResponse>>(projectTransaction);
        }

        public List<TransactionResponse> GetVerifiedTransactions()
        {
            return _mapper.Map<List<TransactionResponse>>(
                _transactionRepository.GetAll().Where(x => x.IsVerified && x.IsActive));
        }


        public bool IsExistingTransaction(int memberId, DateTime transactionDate, int transactionType)
        {
            /*            List<Transaction> transactions = _transactionRepository.GetAll()
                            .Where(x => x.TransactionType == transactionType).ToList();

                        var firstDayOfMonth = new DateTime(transactionDate.Year, transactionDate.Month, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                        return _transactionRepository.IsExistingTransaction(memberId, transactionDate, transactionType);
            */
            return false;        
        }

        private bool SaveData(List<Transaction> transaction, Account accountDetails, Member memberDetails)
        {
            using (var ts = new TransactionScope())
            {
                using (var context = _context)
                {
                    try
                    {
                        if (transaction != null)
                        {
                            foreach (var t in transaction)
                            {
                                _transactionRepository.Add(t);
                            }

                            //_transactionRepository.AddAll(transaction);

                            var memberAccountInfo = accountDetails;

                            List<Transaction> memberTransactions = _mapper.Map<List<Transaction>>
                                (_transactionRepository.GetTransactionsByMemberId(memberDetails.Id));

                            var depositAmount = memberTransactions
                                .Where(d => d.TransactionType == TransactionType.Deposit)
                                .Sum(x => x.TransactionAmounts);

                            var withdrawAmount = memberTransactions
                                .Where(d => d.TransactionType == TransactionType.Withdraw)
                                .Sum(x => x.TransactionAmounts);

                            var payableAmounts = memberTransactions
                                .Where(d => d.TransactionType == TransactionType.Deposit)
                                .Sum(x => x.PayableAmounts);

                            memberAccountInfo.TotalAmounts = _transactionRepository
                                .GetAll().Sum(ta => ta.TransactionAmounts);

                            memberAccountInfo.DueAmounts = _transactionRepository
                                .GetAll().OrderByDescending(t => t.TransactionDate)
                                .FirstOrDefault().DueAmounts;
                                                        
                            var accountTransaction = _mapper.Map<AccountRequest>(memberAccountInfo);
                            var updateAcountTransaction = _accountService.SaveAccount(accountTransaction);

                            var memberAccountsInfo = _mapper.Map<List<Account>>
                                (_accountService.GetAccountsByMember(memberDetails.Id));

                            memberDetails.NumberOfAccount = memberAccountsInfo.Count;

                            memberDetails.TotalAmounts = memberAccountsInfo.Sum(x => x.TotalAmounts);

                            var memberTransaction = _mapper.Map<MemberRequest>(memberDetails);
                            var updateMemberTransaction = _memberService.SaveMember(memberTransaction);
                        }
                    }
                    catch (Exception ex)
                    {
                        ts.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
                ts.Complete();
            }
            return true;
        }

        public TransactionResponse SaveTransaction(TransactionRequest transaction)
        {
            List<Transaction> saveTransactions = new List<Transaction>();

            Account accountDetails = new Account();
            Member memberDetails = new Member();

            accountDetails = _accountRepository.GetById(transaction.AccountId);
            memberDetails = _memberRepository.GetById(transaction.MemberId);

            var memberAllTransaction = GetTransactionsByMemberId(transaction.MemberId).ToList();

            var totalDepositedAmount = memberAllTransaction.Count > 0 
                ? memberAllTransaction.Where(d => d.AccountId == transaction.AccountId 
                && d.TransactionType == TransactionType.Deposit).Sum(s => s.TransactionAmounts)
                : 0;

            if (transaction.TransactionType == TransactionType.Deposit)
            {
                saveTransactions = DepositTransaction(transaction, memberAllTransaction, accountDetails);
            }

            if (transaction.TransactionType == TransactionType.Withdraw)
            {
                var withdrawTransaction = new Transaction();
                if (transaction.TransactionAmounts == totalDepositedAmount)
                {
                    saveTransactions.Add(_mapper.Map<Transaction>(transaction));
                }
                saveTransactions.Add(_mapper.Map<Transaction>(transaction));
            }

            if (transaction.TransactionType == TransactionType.Cost)
            {
                saveTransactions.Add(_mapper.Map<Transaction>(transaction));
            }

            var returnValue = SaveData(saveTransactions, accountDetails, memberDetails);
            if(returnValue == true)
            {
                return _mapper.Map<TransactionResponse>(transaction);
            }
            else
            {
                return _mapper.Map<TransactionResponse>(transaction);
            }
        }

        public List<Transaction> DepositTransaction(TransactionRequest transaction, 
            List<TransactionResponse> memberAllTransaction, Account accountDetails)
        {
            int numOfMonths = 0;
            var projectInfo = _projectService.GetProject(transaction.ProjectId);
            var transactionDate = transaction.TransactionDate;
            var dueAmounts = transaction.DueAmounts;
            var payableAmounts = (double)accountDetails.PayableAmounts;
            var transactionAmounts = transaction.TransactionAmounts;

            List<Transaction> depositTransaction = new List<Transaction>();

            //For First Installment
            if (memberAllTransaction.Count == 0)
            {
                numOfMonths = Math.Abs(12 * (transaction.TransactionDate.Date.Year - projectInfo.StartDate.Date.Year)
                        + transaction.TransactionDate.Date.Month - projectInfo.StartDate.Date.Month);

                var amounts = payableAmounts * numOfMonths;

                var partialAmounts = transactionAmounts % payableAmounts;
                var monthlyAmounts = transactionAmounts - partialAmounts;
                var numofInstallment = Math.Ceiling((double)transactionAmounts / (double)payableAmounts);

                if (transactionAmounts <= amounts)
                {
                    for (int installment = 1; installment <= numofInstallment; installment++)
                    {
                        transaction.InstallmentNo = installment;

                        if (installment == numofInstallment && partialAmounts != 0)
                        {
                            transaction.TransactionAmounts = partialAmounts;
                        }
                        else
                        {
                            transaction.TransactionAmounts = payableAmounts;
                        }

                        if (installment == 1)
                        {
                            transaction.TransactionDate = accountDetails.OpaningDate;
                        }
                        else
                        {
                            transaction.TransactionDate = depositTransaction
                                .OrderByDescending(d => d.TransactionDate)
                                .FirstOrDefault()
                                .TransactionDate.AddMonths(1);
                        }
                        
                        if (installment == numofInstallment)
                        {
                            transaction.DueAmounts = Math.Abs(amounts - transactionAmounts);
                        }
                        else
                        {
                            transaction.DueAmounts = 0;
                        }

                        depositTransaction.Add(_mapper.Map<Transaction>(transaction));
                    }
                }
                else if (transactionAmounts >= amounts)
                {
                    for (int installment = 1; installment <= numofInstallment; installment++)
                    {
                        transaction.InstallmentNo = installment;
                        transaction.TransactionAmounts = payableAmounts;
                        transaction.TransactionDate = transactionDate.AddMonths(installment);

                        depositTransaction.Add(_mapper.Map<Transaction>(transaction));
                    }
                }
            }
            //For Second or other Installment
            else
            {
                var memberLastTransaction = memberAllTransaction
                    .OrderByDescending(x => x.TransactionDate)
                    .FirstOrDefault();

                //Last Transaction update
                var lastTransactionDate = memberLastTransaction.TransactionDate;
                var lastTransactionAmount = memberLastTransaction.TransactionAmounts;
                var payLastTransactionAmount = memberLastTransaction.PayableAmounts - lastTransactionAmount;

                memberLastTransaction.TransactionDate = lastTransactionDate;
                memberLastTransaction.TransactionAmounts = lastTransactionAmount + payLastTransactionAmount;
                memberLastTransaction.DueAmounts = 0;

                _transactionRepository.Update(_mapper.Map<Transaction>(memberLastTransaction));


                transactionAmounts = transactionAmounts - payLastTransactionAmount;

                numOfMonths = Math.Abs(12 * (transactionDate.Year - lastTransactionDate.Date.Year)
                    + transactionDate.Month - lastTransactionDate.Date.Month);

                var amounts = payableAmounts * numOfMonths;

                var partialAmounts = transactionAmounts % payableAmounts;
                var monthlyAmounts = transactionAmounts - partialAmounts;
                var numofInstallment = Math.Ceiling((double)transactionAmounts / (double)payableAmounts);

                if (transactionAmounts <= amounts)
                {
                    for (int installment = 1; installment <= numofInstallment; installment++)
                    {
                        transaction.InstallmentNo = memberLastTransaction.InstallmentNo + installment;

                        if (installment == numofInstallment && partialAmounts != 0)
                        {
                            transaction.TransactionAmounts = partialAmounts;
                        }
                        else
                        {
                            transaction.TransactionAmounts = payableAmounts;
                        }

                        transaction.TransactionDate = memberLastTransaction
                            .TransactionDate.AddMonths(installment);

                        transaction.DueAmounts = 0;

                        if (installment == numofInstallment)
                        {
                            transaction.DueAmounts = Math.Abs(amounts - transactionAmounts);
                        }
                        else
                        {
                            transaction.DueAmounts = 0;
                        }

                        depositTransaction.Add(_mapper.Map<Transaction>(transaction));
                    }
                }
                else
                {
                    for (int installment = 1; installment <= numofInstallment; installment++)
                    {
                        transaction.InstallmentNo = memberLastTransaction.InstallmentNo + installment;

                        if (installment == numofInstallment && partialAmounts != 0)
                        {
                            transaction.TransactionAmounts = partialAmounts;
                        }
                        else
                        {
                            transaction.TransactionAmounts = payableAmounts;
                        }

                        transaction.TransactionDate = memberLastTransaction
                            .TransactionDate.AddMonths(installment);
                        transaction.DueAmounts = 0;

                        depositTransaction.Add(_mapper.Map<Transaction>(transaction));
                    }
                }
            }
            return depositTransaction;
        }

       
        public void DeleteTransaction(int id)
        {
            var transactionInfo = GetTransaction(id);
            var memberAccountInfo = _accountService
                .GetAccountsByMember(transactionInfo.MemberId)
                .FirstOrDefault(x => x.Id == transactionInfo.AccountId);

            var getMemberAccounts = _accountService
                .GetAccountsByMember(transactionInfo.MemberId);

            var getMember = _memberService.GetMember(transactionInfo.MemberId);

            memberAccountInfo.TotalAmounts = memberAccountInfo.TotalAmounts - transactionInfo.TransactionAmounts;

            _accountService.SaveAccount(_mapper.Map<AccountRequest>(memberAccountInfo));

            getMember.TotalAmounts = (decimal?)getMemberAccounts.Sum(x => x.TotalAmounts);

            _memberService.SaveMember(_mapper.Map<MemberRequest>(getMember));

            _transactionRepository.Delete(_transactionRepository.GetById(id));
        }
    }
}
