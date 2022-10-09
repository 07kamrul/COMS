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
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public int InstallmentNo { get; set; }
        public decimal DueAmounts { get; set; }
        public decimal PayableAmounts { get; set; }
        public decimal TransactionAmounts { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
