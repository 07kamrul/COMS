using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class DepositResponse
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public virtual MemberResponse Member { get; set; }
        public int AmountId { get; set; }
        public virtual AmountResponse Amounts { get; set; }
        public DateTime DepositeDate { get; set; }
        public List<AttachmentResponse> Attachments { get; set; }

    }
}
