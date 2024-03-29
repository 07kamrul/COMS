﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Attachments")]
    public class Attachment : BaseEntity
    {
        public int AttachmentTypeId { get; set; }
        public string FileGUID { get; set; }
        public virtual AttachmentType AttachmentType { get; set; }
        public string FileName { get; set; }
        public int MemberId { get; set; }
    }
}
