using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Amounts : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Members? Members { get; set; }
        public int Amount { get; set; }
        public DateTime AmountDate { get; set; }
        public int DipositeId { get; set; }
        public virtual Deposites Deposites{ get; set; }
        public bool IsVerified { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
    }
}
