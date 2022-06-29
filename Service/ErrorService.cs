using Core.Repository;
using Core.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ErrorService : IErrorService
    {
        IErrorRepository _errorRepository;

        public ErrorService(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public List<Error> GetErrors(int errorId)
        {
            return _errorRepository.GetErrors(errorId);
        }

        public void SaveError(Error item)
        {
            _errorRepository.SaveError(item);
        }
    }

    public class NullErrorService : IErrorService
    {
        public List<Error> GetErrors(int errorId)
        {
            return null;
        }

        public void SaveError(Error item)
        {
            
        }
    }
}
