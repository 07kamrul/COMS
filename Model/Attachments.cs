using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Attachments")]
    public class Attachments : BaseEntity
    {
        public int AttachmentTypeId { get; set; }
        public string FileGUID { get; set; }
        public virtual AttachmentTypes AttachmentType { get; set; }
        public string FileName { get; set; }
        public int MemberId { get; set; }
    }
}
