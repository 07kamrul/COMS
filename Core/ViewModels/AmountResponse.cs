using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class AmountResponse
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public virtual MemberResponse Member { get; set; }
        public int Amount { get; set; }
        public DateTime AmountDate { get; set; }
        public int DepositeId { get; set; }
        public virtual DepositResponse Deposit { get; set; }
    }
}
