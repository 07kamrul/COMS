using Core.RequestModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public List<RoleReqquestModel> Roles { get; set; }
    }
}
