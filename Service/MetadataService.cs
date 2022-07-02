using AutoMapper;
using Core.Repository;
using Core.Service;
using Core.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MetadataService : IMetadataService
    {
        private readonly IMapper _mapper;
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAmountRepository _amountRepository;
        private readonly IDepositRepository _depositRepository;
        private readonly IMemberRepository _memberRepository;

        public MetadataService(IMapper mapper, 
            IAttachmentTypeRepository attachmentTypeRepository,
            IUserRepository userRepository, 
            IAmountRepository amountRepository, 
            IDepositRepository depositRepository,
            IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _attachmentTypeRepository = attachmentTypeRepository;
            _userRepository = userRepository;
            _amountRepository = amountRepository;
            _depositRepository = depositRepository;
            _memberRepository = memberRepository;
        }

        public List<AttachmentTypeResponse> GetAttachmentTypes()
        {
            var response = _attachmentTypeRepository.GetAll().ToList();
            var mappedResponse = _mapper.Map<List<AttachmentTypeResponse>>(response);
            return mappedResponse;
        }

        public AmountResponse GetAmount(int id)
        {
            return _mapper.Map<AmountResponse>(_amountRepository.GetById(id));
        }

        public List<AmountResponse> GetAmounts()
        {
            return _mapper.Map<List<AmountResponse>>(_amountRepository.GetAll().ToList());
        }

        public DepositResponse GetDeposit(int id)
        {
            return _mapper.Map<DepositResponse>(_depositRepository.GetById(id));
        }

        public List<DepositResponse> GetDeposits()
        {
            return _mapper.Map<List<DepositResponse>>(_depositRepository.GetAll().ToList());
        }

        public MemberResponse GetMember(int id)
        {
            return _mapper.Map<MemberResponse>(_memberRepository.GetById(id));
        }

        public List<MemberResponse> GetMembers()
        {
            return _mapper.Map<List<MemberResponse>>(_memberRepository.GetAll().ToList());
        }
    }
}
