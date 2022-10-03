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
            List<Account> accountsByMember = _accountRepository.GetInactiveAccounts();
            return _mapper.Map<List<AccountResponse>>(accountsByMember);
        }

        public List<AccountResponse> GetRequestVerifyAccounts()
        {
            List<Account> accountsByMember = _accountRepository.GetRequestVerifyAccounts();
            return _mapper.Map<List<AccountResponse>>(accountsByMember);
        }

        public List<AccountResponse> GetVerifiedAccounts()
        {
            List<Account> accountsByMember = _accountRepository.GetVerifiedAccounts();
            return _mapper.Map<List<AccountResponse>>(accountsByMember);
        }

        public List<AccountResponse> GetAccountsByProject(int id)
        {
            List<Account> accountsByMember = _accountRepository.GetAccountsByProject(id);
            return _mapper.Map<List<AccountResponse>>(accountsByMember);
        }

        public AccountResponse SaveAccount(AccountRequest accountModel)
        {
            Account existingAccount = _accountRepository
                .FindBy(x => x.Id == accountModel.Id 
                && x.MemberId == accountModel.MemberId 
                && x.ProjectId == accountModel.ProjectId)
                .FirstOrDefault();

            Account account = _mapper.Map<Account>(accountModel);
            account.MemberId = accountModel.MemberId;
            account.ProjectId = accountModel.ProjectId;
            account.IsActive = true;

            if (existingAccount == null)
            {
                account.Id = 0;
                return _mapper.Map<AccountResponse>(_accountRepository.Add(account));
            }
            else
            {
                existingAccount = _mapper.Map<Account>(accountModel);

                return _mapper.Map<AccountResponse>(_accountRepository.Update(existingAccount));
            }
        }

        public void DeleteAccount(int id)
        {
            _accountRepository.Delete(_accountRepository.GetById(id));
        }

    }
}
