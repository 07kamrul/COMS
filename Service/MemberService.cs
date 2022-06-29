using AutoMapper;
using Core.Common;
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
    public class MemberService : IMemberService
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileStore _fileStore;
        private readonly IConfiguration _configuration;

        public MemberService(IMapper mapper, IMemberRepository memberRepository, IAttachmentRepository attachmentRepository,
            IConfiguration configuration, IFileStore fileStore)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _attachmentRepository = attachmentRepository;
            _configuration = configuration;
            _fileStore = fileStore;
        }

        public MemberResponse GetMember(int id)
        {
            return _mapper.Map<MemberResponse>(_memberRepository.GetById(id));
        }

        public List<MemberResponse> GetMembers()
        {
            return _mapper.Map<List<MemberResponse>>(_memberRepository.GetAll());
        }

        public MemberResponse SaveMember(MemberRequestModel memberRequestModel)
        {
            Members existingMember = _memberRepository.FindBy(x => x.Email == memberRequestModel.Email || x.Phone == memberRequestModel.Phone).FirstOrDefault();

            Members member = _mapper.Map<Members>(memberRequestModel);
            member.Email = memberRequestModel.Email;
            member.Phone = memberRequestModel.Phone;
            member.IsActive = true;

            if(existingMember == null)
            {
                member.Id = 0;
                return _mapper.Map<MemberResponse>(_memberRepository.Add(member));
            }

            member.Id = existingMember.Id;
            return _mapper.Map<MemberResponse>(member);
        }

        public Page<MemberResponse> Search(MemberSearchRequestModel searchModel, int skip, int take)
        {
            Page<Members> members = _memberRepository.Search(searchModel, skip, take);
            return new Page<MemberResponse>
            {
                Data = _mapper.Map<List<MemberResponse>>(members.Data),
                Total = members.Total
            };
        }

        public void UpdateMember(MemberRequestModel memberRequestModel)
        {
            _memberRepository.Update(_mapper.Map<Members>(memberRequestModel));
        }

        public void DeleteMember(int id)
        {
            _memberRepository.Delete(_memberRepository.GetById(id));
        }

        public Stream GetAttachmentFile(int attachmentId)
        {
            string attachmentPath = _configuration["LocalFileStore:Path"];
            Attachment attachment = _attachmentRepository.GetById(attachmentId);
            try
            {
                return _fileStore.ReadFile(Path.Combine(attachmentPath, attachment.FileGUID));
            }
            catch(Exception ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
        }

        public void VerifyMember(int memberId, bool isVerify)
        {
            Members members = _memberRepository.GetById(memberId);
            members.IsVerified = isVerify;
            members.VerificationDate = DateTime.Now;
            _memberRepository.Update(members);
        }
    }
}
