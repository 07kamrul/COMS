using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Deposites : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Members? Members { get; set; }
        public int AmountId { get; set; }
        public virtual Amounts Amounts { get; set; }
        public DateTime DepositeDate { get; set; }
        public bool IsVerified { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
