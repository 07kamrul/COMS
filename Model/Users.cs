using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Users")]
    public class Users : BaseEntity
    {
        public string Email { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int? GroupId { get; set; }
        public string RefreshToken { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
    }
}
