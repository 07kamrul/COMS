using Core.RequestModels;
using Core.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IDepositService
    {
        Page<DepositResponse> Search(DepositResponse searchModel, int skip, int take);
        DepositResponse SaveDeposit(DepositRequestModel depositRequestModel);
        List<DepositResponse> GetDeposits();
        DepositResponse GetDeposit(int id);
        void DeleteDeposit(int id);
        void UpdateDeposit(DepositRequestModel depositRequestModel);
        Stream GetAttachmentFile(int id);
        void VerifyDeposit(int amountId, bool isVerify);
        bool IsUnique(string memberId);
        List<string> CheckUnique(string memberId);
    }
}
