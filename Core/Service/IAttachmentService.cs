using Core.RequestModels;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IAttachmentService
    {
        AttachmentResponse SaveAttachment(AttachmentRequest attachmentRequestModel);
        List<AttachmentResponse> GetAttachments();
        List<AttachmentResponse> GetAttachmentByMemberId(int memberId);
        AttachmentResponse GetAttachment(int attachmentId);
        Stream GetAttachmentFile(int attachmentId);
        void DeleteAttachment(int attachmentId);

    }
}
