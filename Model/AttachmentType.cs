using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("AttachmentTypes")]

    public class AttachmentType : BaseEntity
    {
        public string Title { get; set; }
    }
}
