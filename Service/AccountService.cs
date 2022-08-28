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
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileStore _fileStore;
        private readonly IConfiguration _configuration;


        public AccountService(IMapper mapper, IAccountRepository accountRepository, 
            IAttachmentRepository attachmentRepository, IFileStore fileStore, IConfiguration configuration)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _attachmentRepository = attachmentRepository;
            _fileStore = fileStore;
            _configuration = configuration;
        }

        public AccountResponse GetAccount(int id)
        {
            return _mapper.Map<AccountResponse>(_accountRepository.GetById(id));
        }

        public List<AccountResponse> GetAccounts()
        {
            return _mapper.Map<List<AccountResponse>>(_accountRepository.GetAll());
        }

        public List<AccountResponse> GetAccountsByMember(int id)
        {
            List<Account> accountsByMember = _accountRepository.GetAccountsByMember(id);
            return _mapper.Map<List<AccountResponse>>(accountsByMember);
        }

        public List<AccountResponse> GetInactiveAccounts()
        {
            return null;
        }

        public List<AccountResponse> GetRequestVerifyAccounts()
        {
            return null;
        }

        public List<AccountResponse> GetVerifiedAccounts()
        {
            return null;
        }

        public AccountResponse SaveAccount(AccountRequest account)
        {
            return null;
        }
        public void DeleteAccount(int id)
        {
        }
    }
}
