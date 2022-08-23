using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class TransactionRequest
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public int TransactionAmounts { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
