using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IErrorRepository
    {
        Error SaveError(Error item);
        List<Error> GetErrors(int errorId);
    }
}
