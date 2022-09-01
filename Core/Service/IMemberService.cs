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
        Page<MemberResponse> Search(MemberSearchRequest searchModel, int skip, int take);
        MemberResponse SaveMember(MemberRequest memberRequestModel);
        List<MemberResponse> GetMembers();
        MemberResponse GetMember(int id);
        void DeleteMember(int id);
        void UpdateMember(MemberRequest memberRequestModel);
        Stream GetAttachmentFile(int id);
        bool IsExistingMember(string email, int code, string phone, long nid);
        List<MemberResponse> GetVerifiedMembers();
        List<MemberResponse> GetRequestVerifyMembers();
        List<MemberResponse> GetInactiveMembers();
        List<MemberResponse> GetActiveMembers();
        MemberResponse VerifyMember(MemberVerifyRequest member);
    }
}
