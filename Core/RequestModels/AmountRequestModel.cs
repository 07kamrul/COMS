using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class AmountRequestModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int Amount { get; set; }
        public DateTime AmountDate { get; set; }
        public int DipositeId { get; set; }
    }
}
