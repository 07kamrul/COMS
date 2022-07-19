

namespace Model
{
    public class RoleUser : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }
    }
}
