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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AmountService : IAmountService
    {
        private readonly IMapper _mapper;
        private readonly IAmountRepository _amountRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileStore _fileStore;
        private readonly IConfiguration _configuration;

        public AmountService(IMapper mapper, IAmountRepository amountRepository, IAttachmentRepository attachmentRepository, IFileStore fileStore, IConfiguration configuration)
        {
            _mapper = mapper;
            _amountRepository = amountRepository;
            _attachmentRepository = attachmentRepository;
            _fileStore = fileStore;
            _configuration = configuration;
        }

        public AmountResponse GetAmount(int id)
        {
            return _mapper.Map<AmountResponse>(_amountRepository.GetById(id));
        }

        public List<AmountResponse> GetAmounts()
        {
            return _mapper.Map<List<AmountResponse>>(_amountRepository.GetAll());
        }

        public List<AmountResponse> GetAmountsByMemberId(int memberId)
        {
            return _mapper.Map<List<AmountResponse>>(_amountRepository.GetAll().Where(x => x.MemberId == memberId).ToList());
        }

        public AmountResponse GetAmountByDepositId(int depositId)
        {
            return _mapper.Map<AmountResponse>(_amountRepository.GetAll().Where(x => x.DipositeId == depositId).ToList());
        }

        public AmountResponse SaveAmount(AmountRequestModel amountRequestModel)
        {
            Amounts existingAmounts = _amountRepository.FindBy(x => x.MemberId == amountRequestModel.MemberId && x.DipositeId == amountRequestModel.DipositeId).FirstOrDefault();

            Amounts amounts = _mapper.Map<Amounts>(amountRequestModel);
            amounts.MemberId = amountRequestModel.MemberId;
            amounts.DipositeId = amountRequestModel.DipositeId;
            amounts.IsActive = true;

            if(existingAmounts == null)
            {
                amounts.Id = 0;
                return _mapper.Map<AmountResponse>(_amountRepository.Add(amounts));
            }

            amounts.Id = 0;
            return _mapper.Map<AmountResponse>(amounts);
        }

        public void UpdateAmount(AmountRequestModel amountRequestModel)
        {
            _amountRepository.Update(_mapper.Map<Amounts>(amountRequestModel));
        }

        public void DeleteAmount(int id)
        {
            _amountRepository.Delete(_amountRepository.GetById(id));
        }

        public Stream GetAttachmentFile(int id)
        {
            string attachmentPath = _configuration["LocalFileStore:Path"];
            Attachment attachment = _attachmentRepository.GetById(id);

            try
            {
                return _fileStore.ReadFile(Path.Combine(attachmentPath, attachment.FileGUID));
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
        }

        public void VerifyAmount(int amountId, bool isVerify)
        {
            Amounts amounts = _amountRepository.GetById(amountId);
            amounts.IsVerified = isVerify;
            amounts.VerificationDate = DateTime.Now;
            _amountRepository.Update(amounts);
        }
    }
}
