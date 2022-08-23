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
        private readonly IProjectRepository _projectRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMemberRepository _memberRepository;

        public MetadataService(IMapper mapper, 
            IAttachmentTypeRepository attachmentTypeRepository,
            IUserRepository userRepository, 
            IProjectRepository projectRepository,
            ITransactionRepository transactionRepository,
            IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _attachmentTypeRepository = attachmentTypeRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _transactionRepository = transactionRepository;
            _memberRepository = memberRepository;
        }

        public List<AttachmentTypeResponse> GetAttachmentTypes()
        {
            var response = _attachmentTypeRepository.GetAll().ToList();
            var mappedResponse = _mapper.Map<List<AttachmentTypeResponse>>(response);
            return mappedResponse;
        }

        public ProjectResponse GetProject(int id)
        {
            return _mapper.Map<ProjectResponse>(_projectRepository.GetById(id));
        }
        
        public List<ProjectResponse> GetProjectsByMemberId(int memberId)
        {
            return _mapper.Map<List<ProjectResponse>>(_projectRepository.GetAll().Where(x => x.MemberId == memberId).ToList());
        }

        public List<ProjectResponse> GetProjects()
        {
            return _mapper.Map<List<ProjectResponse>>(_projectRepository.GetAll().ToList());
        }

        public TransactionResponse GetTransaction(int id)
        {
            return _mapper.Map<TransactionResponse>(_transactionRepository.GetById(id));
        }

        public List<TransactionResponse> GetTransactions()
        {
            return _mapper.Map<List<TransactionResponse>>(_transactionRepository.GetAll().ToList());
        }
        public List<TransactionResponse> GetTransactionsByMemberId(int memberId)
        {
            return _mapper.Map<List<TransactionResponse>>(_transactionRepository.GetAll().Where(x => x.MemberId == memberId).ToList());
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
