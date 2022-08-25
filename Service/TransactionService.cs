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
            throw new NotImplementedException();
        }

        public TransactionResponse GetTransaction(int id)
        {
            return _mapper.Map<TransactionResponse>(_transactionRepository.GetById(id));
        }


        public List<TransactionResponse> GetTransactions()
        {
            return _mapper.Map<List<TransactionResponse>>(_transactionRepository.GetAll());
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
            var transactionDate = transaction.TransactionDate;
            var transactionAmount = transaction.TransactionAmounts;


        }
    }
}
