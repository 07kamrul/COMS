using AutoMapper;
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
    public class DepositService : IDepositService
    {
        private readonly IMapper _mapper;
        private readonly IDepositRepository _depositRepository;


        public Stream GetAttachmentFile(int id)
        {
            throw new NotImplementedException();
        }

        public DepositResponse GetDeposit(int id)
        {
            throw new NotImplementedException();
        }

        public List<DepositResponse> GetDeposits()
        {
            throw new NotImplementedException();
        }

        public DepositResponse SaveDeposit(DepositRequestModel depositRequestModel)
        {
            throw new NotImplementedException();
        }

        public Page<DepositResponse> Search(DepositResponse searchModel, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public void UpdateDeposit(DepositRequestModel depositRequestModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteDeposit(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> CheckUnique(string memberId)
        {
            throw new NotImplementedException();
        }

        public void VerifyDeposit(int amountId, bool isVerify)
        {
            throw new NotImplementedException();
        }

        public bool IsUnique(string memberId)
        {
            throw new NotImplementedException();
        }
    }
}
