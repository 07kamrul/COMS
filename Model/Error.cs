using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Error : BaseEntity
    {
        public string ErrorMessage { get; set; }
        public string ApiUrl { get; set; }
    }
}
