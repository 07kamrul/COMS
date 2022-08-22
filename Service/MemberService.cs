using AutoMapper;
using Core.Common;
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
            var getMembers = _memberRepository.GetAll().Where(x => x.IsActive);
            return _mapper.Map<List<MemberResponse>>(getMembers);
        }

        public bool IsExistingMember(string email, int code, string phone, long nid)
        {
            return _memberRepository.IsExistingMember(email, code, phone, nid);
        }

        public MemberResponse SaveMember(MemberRequestModel memberRequestModel)
        {
            Member existingMember = _memberRepository.FindBy(x => x.Email == memberRequestModel.Email || x.Phone == memberRequestModel.Phone || x.NID == memberRequestModel.NID).FirstOrDefault();

            Member member = _mapper.Map<Member>(memberRequestModel);
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
            Page<Member> members = _memberRepository.Search(searchModel, skip, take);
            return new Page<MemberResponse>
            {
                Data = _mapper.Map<List<MemberResponse>>(members.Data),
                Total = members.Total
            };
        }

        public void UpdateMember(MemberRequestModel memberRequestModel)
        {
            _memberRepository.Update(_mapper.Map<Member>(memberRequestModel));
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
            Member members = _memberRepository.GetById(memberId);
            members.IsVerified = isVerify;
            members.VerificationDate = DateTime.Now;
            _memberRepository.Update(members);
        }

        public List<MemberResponse> GetVerifiedMembers()
        {
            var getVerifiedMembers = _memberRepository.GetAll().Where(x => x.IsVerified && x.IsActive);
            return _mapper.Map<List<MemberResponse>>(getVerifiedMembers);
        }

        public List<MemberResponse> GetRequestVerifyMembers()
        {
            var getRequestVerifyMembers = _memberRepository.GetAll().Where(x => !x.IsVerified && x.IsActive);
            return _mapper.Map<List<MemberResponse>>(getRequestVerifyMembers);
        }

        public List<MemberResponse> GetInactiveMembers()
        {
            var getInactiveMembers = _memberRepository.GetAll().Where(x => !x.IsActive);
            return _mapper.Map<List<MemberResponse>>(getInactiveMembers);
        }
    }
}
