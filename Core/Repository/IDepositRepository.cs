using Core.RequestModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IDepositRepository : IBaseRepository<Deposites>
    {
        Page<Deposites> Search(DepositSearchRequestModel searchModel, int skip, int take);

    }
}
