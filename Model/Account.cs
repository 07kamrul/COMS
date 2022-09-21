using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    [Table("Accounts")]
    public class Account : BaseEntity
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public DateTime OpaningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public decimal? DueAmounts { get; set; }
        public decimal PayableAmounts { get; set; }
        public decimal? TotalAmounts { get; set; }
        public bool IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
    }
}
