using System.Collections.Generic;

namespace Model
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
