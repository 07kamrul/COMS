using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class DepositSearchRequestModel
    {
        public int DepositId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int AmountId { get; set; }
        public int Amount { get; set; }
        public DateTime? DepositeDate { get; set; }
    }
}
