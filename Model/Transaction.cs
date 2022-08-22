using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Transactions")]
    public class Transaction : BaseEntity
    {
        public int MemberId { get; set; }
        public int TransactionAmounts { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
