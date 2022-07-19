using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Roles")]
    public class Roles : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
