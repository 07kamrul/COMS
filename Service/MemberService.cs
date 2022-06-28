using AutoMapper;
using Core.Common;
using Core.Repository;
using Core.RequestModels;
using Core.Service;
using Core.ViewModel;
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
        IMapper _mapper;
        IMemberRepository _memberRepository;

        public MemberService(IMapper mapper, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
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

        public Stream GetAttachmentFile(int id)
        {
            throw new NotImplementedException();
        }
    }
}
