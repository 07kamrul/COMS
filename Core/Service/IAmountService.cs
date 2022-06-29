using Core.RequestModels;
using Core.ViewModel;
using Model;
using System.Collections.Generic;
using System.IO;

namespace Core.Service
{
    public interface IAmountService
    {
        Page<AmountResponse> Search(AmountRequestModel searchModel, int skip, int take);
        AmountResponse SaveAmount(AmountRequestModel amountRequestModel);
        List<AmountResponse> GetAmounts();
        List<AmountResponse> GetAmountsByMemberId(int memberId);
        AmountResponse GetAmount(int id);
        AmountResponse GetAmountByDepositId(int depositId);
        void DeleteAmount(int id);
        void UpdateAmount(AmountRequestModel amountRequestModel);
        Stream GetAttachmentFile(int id);
        void VerifyAmount(int amountId, bool isVerify);
        bool IsUnique(string memberId);
        List<string> CheckUnique(string memberId);

    }
}
