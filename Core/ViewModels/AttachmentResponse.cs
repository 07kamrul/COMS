﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class AttachmentResponse
    {
        public int Id { get; set; }
        public string FileGUID { get; set; }
        public int AttachmentTypeId { get; set; }
        public int MemberId { get; set; }
        public string FileName { get; set; }
    }
}
