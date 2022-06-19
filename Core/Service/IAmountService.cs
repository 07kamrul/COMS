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
        AmountResponse getAmount(int id);
        void DeleteAmount(int id);
        void UpdateAmount(AmountRequestModel amountRequestModel);
        Stream GetAttachmentFile(int id);
        void VerifyAmount(int amountId, bool isVerify);
        bool IsUnique(string memberId);
        List<string> CheckUnique(string memberId);

    }
}
