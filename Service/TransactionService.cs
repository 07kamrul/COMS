using AutoMapper;
using Core.FileStore;
using Core.Repository;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileStore _fileStore;
        private readonly IConfiguration _configuration;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository,
            IAttachmentRepository attachmentRepository, IFileStore fileStore, IConfiguration configuration)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _attachmentRepository = attachmentRepository;
            _fileStore = fileStore;
            _configuration = configuration;
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
            return _mapper.Map<List<TransactionResponse>>(_transactionRepository.GetAll());
        }

        public List<TransactionResponse> GetTransactionsByMemberId(int memberId)
        {
            List<Transaction> memberTransaction = _transactionRepository.GetTransactionsByMemberId(memberId);

            return _mapper.Map<List<TransactionResponse>>(memberTransaction);
        }

        public List<TransactionResponse> GetTransactionsByProject(int projectId)
        {
            List<Transaction> projectTransaction = _transactionRepository.GetTransactionsByProject(projectId);

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


        public TransactionResponse SaveTransaction(TransactionRequest transaction)
        {
            var TDate = transaction.TransactionDate;
            var dueAmounts = transaction.DueAmounts;
            var payableAmounts = transaction.PayableAmounts;
            var transactionAmounts = transaction.TransactionAmounts;
            var memberId = transaction.MemberId;
            var projectId = transaction.ProjectId;
            int numOfMonths = 0;
            
            var memberAllTransaction = GetTransactionsByMemberId(memberId);
            var projectInfo = GetProjectById(projectId);

            if (memberAllTransaction == null)
            {
                int installmentNo = 0;
                var transactionDate = projectInfo.StartDate;

                if (transactionDate <= projectInfo.StartDate)
                {
                    numOfMonths = Math.Abs(12 * (transactionDate.Year - projectInfo.StartDate.Date.Year) 
                        + transactionDate.Month - projectInfo.StartDate.Date.Month);

                    var amounts = payableAmounts * numOfMonths;
                   
                    var partialAmounts = transactionAmounts % payableAmounts;
                    var monthlyAmounts = transactionAmounts - partialAmounts;
                    int numofInstallment = monthlyAmounts / numOfMonths;

                    if (transactionAmounts <= amounts)
                    {
                        for (int installment = 0; installment <= numofInstallment; installment++)
                        {
                            transaction.InstallmentNo = installment;
                            transaction.TransactionAmounts = payableAmounts;
                            transaction.TransactionDate = TDate.AddMonths(installment);
                            if (installment == numofInstallment)
                            {
                                transaction.DueAmounts = Math.Abs(amounts - transactionAmounts);
                            }
                            transaction.DueAmounts = 0;

                            _transactionRepository.Add(_mapper.Map<Transaction>(transaction));
                        }
                    }
                    else if (transactionAmounts >= amounts)
                    {
                        for(int installment = 0; installment <= numofInstallment; installment++)
                        {
                            transaction.InstallmentNo = installment;
                            transaction.TransactionAmounts = payableAmounts;
                            transaction.TransactionDate = TDate.AddMonths(installment);
                     
                            _transactionRepository.Add(_mapper.Map<Transaction>(transaction));
                        }
                    }
                    else
                    {
                        _transactionRepository.Add(_mapper.Map<Transaction>(transaction));
                    }
                }

                Transaction saveTransaction = _transactionRepository.Add(_mapper.Map<Transaction>(transaction));
                return _mapper.Map<TransactionResponse>(saveTransaction);
            }
            else
            {
                var memberLastTransaction = memberAllTransaction.OrderByDescending(x => x.TransactionDate).FirstOrDefault();

            }

        }
    }
}
