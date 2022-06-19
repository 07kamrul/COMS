using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ResponseResult<T> : BaseResponse
    {
        private T result;
        public T Result
        {
            get { return result; }
            set { result = value; }
        }

        public ResponseResult()
        {

        }
    }
}
