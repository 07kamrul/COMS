using Core.RequestModels;
using Core.ViewModels;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IMemberService
    {
        Page<MemberResponse> Search(MemberSearchRequestModel searchModel, int skip, int take);
        MemberResponse SaveMember(MemberRequestModel memberRequestModel);
        List<MemberResponse> GetMembers();
        MemberResponse GetMember(int id);
        void DeleteMember(int id);
        void UpdateMember(MemberRequestModel memberRequestModel);
        Stream GetAttachmentFile(int id);
        void VerifyMember(int memberId, bool isVerify);

    }
}
