using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class AmountSearchRequestModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int AmountId { get; set; }
        public int Amount { get; set; }
        public DateTime? AmountDate { get; set; }
        public int DipositeId { get; set; }
        public bool IsVerified { get; set; }
        public int VerifiedBy { get; set; }
    }
}
