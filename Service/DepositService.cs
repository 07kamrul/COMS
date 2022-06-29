using AutoMapper;
using Core.FileStore;
using Core.Repository;
using Core.RequestModels;
using Core.Service;
using Core.ViewModel;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DepositService : IDepositService
    {
        private readonly IMapper _mapper;
        private readonly IDepositRepository _depositRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileStore _fileStore;
        private readonly IConfiguration _configuration;

        public DepositService(IMapper mapper, IDepositRepository depositRepository, IAttachmentRepository attachmentRepository, IFileStore fileStore, IConfiguration configuration)
        {
            _mapper = mapper;
            _depositRepository = depositRepository;
            _attachmentRepository = attachmentRepository;
            _fileStore = fileStore;
            _configuration = configuration;
        }

        public DepositResponse GetDeposit(int id)
        {
            return _mapper.Map<DepositResponse>(_depositRepository.GetById(id));
        }

        public List<DepositResponse> GetDeposits()
        {
            return _mapper.Map<List<DepositResponse>>(_depositRepository.GetAll());
        }

        public List<DepositResponse> GetDepositsByMemberId(int memberId)
        {
            return _mapper.Map<List<DepositResponse>>(_depositRepository.GetAll().Where(x => x.MemberId == memberId).ToList());
        }

        public DepositResponse SaveDeposit(DepositRequestModel depositRequestModel)
        {
            Deposites existingDeposit = _depositRepository.FindBy(x => x.MemberId == depositRequestModel.MemberId && x.DepositeDate == depositRequestModel.DepositeDate && x.IsActive).FirstOrDefault();
            Deposites deposit = _mapper.Map<Deposites>(depositRequestModel);
            deposit.MemberId = depositRequestModel.MemberId;
            deposit.DepositeDate = depositRequestModel.DepositeDate;
            deposit.IsActive = true;
            
            if(existingDeposit == null)
            {
                deposit.Id = 0;
                return _mapper.Map<DepositResponse>(_depositRepository.Add(deposit));
            }
            deposit.Id = existingDeposit.Id;
            return _mapper.Map<DepositResponse>(deposit);
        }

        public Page<DepositResponse> Search(DepositSearchRequestModel searchModel, int skip, int take)
        {
            Page<Deposites> deposites = _depositRepository.Search(searchModel, skip, take);
            return new Page<DepositResponse>
            {
                Data = _mapper.Map<List<DepositResponse>>(deposites.Data),
                Total = deposites.Total
            };
        }

        public void UpdateDeposit(DepositRequestModel depositRequestModel)
        {
            _depositRepository.Update(_mapper.Map<Deposites>(depositRequestModel));
        }
        public void DeleteDeposit(int id)
        {
            _depositRepository.Delete(_depositRepository.GetById(id));
        }

        public Stream GetAttachmentFile(int id)
        {
            string attachmentPath = _configuration["LocalFileStore:Path"];
            Attachment attachment = _attachmentRepository.GetById(id);
            try
            {
                return _fileStore.ReadFile(Path.Combine(attachmentPath, attachment.FileGUID));
            }
            catch(Exception ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
        }

        public void VerifyDeposit(int depositId, bool isVerify)
        {
            Deposites deposites = _depositRepository.GetById(depositId);
            deposites.IsVerified = isVerify;
            deposites.VerificationDate = DateTime.Now;
            _depositRepository.Update(deposites);
        }
    }
}
