using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IMetadataService
    {
        List<AttachmentTypeResponse> GetAttachmentTypes();
        List<ProjectResponse> GetProjects();
        ProjectResponse GetProject (int id);
        List<TransactionResponse> GetTransactions();
        TransactionResponse GetTransaction(int id);
        List<MemberResponse> GetMembers();
        MemberResponse GetMember(int id);
        List<ProjectResponse> GetProjectsByMemberId(int memberId);
        List<TransactionResponse> GetTransactionsByMemberId(int memberId);
    }
}
