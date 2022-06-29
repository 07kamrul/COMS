using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public class AttachmentRequestModel
    {
        public IFormFile File { get; set; }
        public int AttachmentTypeId { get; set; }
        public int MemberId { get; set; }
        public string FileName { get; set; }
    }
}
