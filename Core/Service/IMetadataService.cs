using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IMetadataService
    {
        List<AmountResponse> GetAmounts();
        AmountResponse GetAmount(int id);
        List<DepositResponse> GetDeposits();
        DepositResponse GetDeposit(int id);
        List<MemberResponse> GetMembers();
        MemberResponse GetMember(int id);

    }
}
