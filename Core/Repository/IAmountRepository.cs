using Core.RequestModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IAmountRepository : IBaseRepository<Amounts>
    {
        Page<Amounts> Search(AmountSearchRequestModel amountSearchRequestModel, int skip, int take);
    }
}
