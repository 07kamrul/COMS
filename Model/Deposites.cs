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
        public int AmountId { get; set; }
        public DateTime DepositeDate { get; set; }
    }
}
