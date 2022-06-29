using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class DepositRequestModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int AmountId { get; set; }
        public DateTime DepositeDate { get; set; }
        public bool IsVerified { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
    }
}
