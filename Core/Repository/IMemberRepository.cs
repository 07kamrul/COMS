using Core.RequestModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IMemberRepository : IBaseRepository<Members>
    {
        Page<Members> Search(MemberSearchRequestModel searchModel, int skip, int take);

    }
}
