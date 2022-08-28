using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class AccountRequest
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public DateTime OpaningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public double DueAmounts { get; set; }
        public double PayableAmounts { get; set; }
        public double TotalAmounts { get; set; }
        public int IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime VerificationDate { get; set; }
    }
}
