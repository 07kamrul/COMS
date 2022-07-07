using Core.RequestModels;
using Core.ViewModels;
using Model;
using System.Collections.Generic;
using System.IO;

namespace Core.Service
{
    public interface IAmountService
    {
        AmountResponse SaveAmount(AmountRequestModel amountRequestModel);
        List<AmountResponse> GetAmounts();
        List<AmountResponse> GetAmountsByMemberId(int memberId);
        AmountResponse GetAmount(int id);
        AmountResponse GetAmountByDepositId(int depositId);
        void DeleteAmount(int id);
        void UpdateAmount(AmountRequestModel amountRequestModel);
        Stream GetAttachmentFile(int id);
        void VerifyAmount(int amountId, bool isVerify);
        Page<AmountResponse> Search(AmountSearchRequestModel amountSearchRequestModel, int skip, int take);
    }
}
