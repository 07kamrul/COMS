using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public DateTime OpaningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public double DueAmounts { get; set; }
        public double PayableAmounts { get; set; }
        public double TotalAmounts { get; set; }
        public bool IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime VerificationDate { get; set; }
    }
}
