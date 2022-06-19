using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public static class CommonValidator
    {
        public static bool IsValidNID(this string NID)
        {
            return NID.Length == 13 || NID.Length == 17 || NID.Length == 10;
        }
        public static bool MAXSecurityInformation()
        {
            return true;
        }
    }
}
