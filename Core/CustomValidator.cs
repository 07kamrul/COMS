using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DateNotInTheFuture : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToDateTime(value).Date > DateTime.Now.Date)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
