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
        List<AmountResponse> GetAmounts();
        AmountResponse GetAmount(int id);
        List<DepositResponse> GetDeposits();
        DepositResponse GetDeposit(int id);
        List<MemberResponse> GetMembers();
        MemberResponse GetMember(int id);
        List<DepositResponse> GetDepositsByMemberId(int memberId);
        List<AmountResponse> GetAmountsByMemberId(int memberId);
    }
}
