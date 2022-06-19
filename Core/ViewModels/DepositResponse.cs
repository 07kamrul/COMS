using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class DepositResponse
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int AmountId { get; set; }
        public DateTime DepositeDate { get; set; }
    }
}
