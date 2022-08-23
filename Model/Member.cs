using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Members")]
    public class Member : BaseEntity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime DateOfBirth{ get; set; }
        public long NID { get; set; }
        public int ProjectId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Nationality{ get; set; }
        public string PresentAddress{ get; set; }
        public string PermanentAddress{ get; set; }
        public string MaritalStatus{ get; set; }
        public string Religion{ get; set; }
        public string Occupation{ get; set; }
        public string Designation{ get; set; }
        public string Company{ get; set; }
        public bool IsVerified { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public List<Attachment> Attachments { get; set; }

    }
}
