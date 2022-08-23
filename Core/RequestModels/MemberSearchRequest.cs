using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class MemberSearchRequest
    {
        public int Code { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int NID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string Occupation { get; set; }
    }
}
