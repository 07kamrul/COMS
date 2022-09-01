using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class MemberVerifyRequest
    {
        public MemberVerifyRequest()
        {
            IsVerified = false;
        }
        public int MemberId { get; set; }
        public bool IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
    }
}
