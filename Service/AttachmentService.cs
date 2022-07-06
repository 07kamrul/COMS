using AutoMapper;
using Core.FileStore;
using Core.Repository;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IMapper _mapper;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IConfiguration _configuration;
        private readonly IFileStore _fileStore;

        public AttachmentService(IMapper mapper, IAttachmentRepository attachmentRepository, IConfiguration configuration, IFileStore fileStore)
        {
            _mapper = mapper;
            _attachmentRepository = attachmentRepository;
            _configuration = configuration;
            _fileStore = fileStore;
        }

        public AttachmentResponse GetAttachment(int attachmentId)
        {
            return _mapper.Map<AttachmentResponse>(_attachmentRepository.GetById(attachmentId));
        }

        public List<AttachmentResponse> GetAttachmentByMemberId(int memberId)
        {
            return _mapper.Map<List<AttachmentResponse>>(_attachmentRepository.GetAll().Where(x => x.MemberId == memberId).ToList());
        }

        public Stream GetAttachmentFile(int attachmentId)
        {
            string attachmentPath = _configuration["LocalFileStore:Path"];
            Attachment attachment = _attachmentRepository.GetById(attachmentId);
            try
            {
                return _fileStore.ReadFile(Path.Combine(attachmentPath, attachment.FileGUID));
            }
            catch(Exception ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
        }

        public List<AttachmentResponse> GetAttachments()
        {
            return _mapper.Map<List<AttachmentResponse>>(_attachmentRepository.GetAll());
        }

        public AttachmentResponse SaveAttachment(AttachmentRequestModel attachmentRequestModel)
        {
            string attachmentPath = _configuration["LocalFileStore:Path"];
            string fileGUID = Guid.NewGuid().ToString();
            IFileInfo fileInfo = new LocalFileInfo
            {
                Name = attachmentRequestModel.FileName,
                FullName = Path.Combine(attachmentPath, fileGUID)
            };
            _fileStore.WriteFile(fileInfo, attachmentRequestModel.File.OpenReadStream());

            Attachment attachment = _mapper.Map<Attachment>(attachmentRequestModel);
            attachment.FileGUID = fileGUID;

            return _mapper.Map<AttachmentResponse>(_attachmentRepository.Add(attachment));
        }
        public void DeleteAttachment(int attachmentId)
        {
            string attachmentPath = _configuration["LoaclFileStore:Path"];
            Attachment attachment = _attachmentRepository.GetById(attachmentId);
            _attachmentRepository.Delete(attachment);
            IFileInfo fileInfo = new LocalFileInfo
            {
                Name = attachment.FileName,
                FullName = Path.Combine(attachmentPath, attachment.FileGUID)
            };
            _fileStore.DeleteFile(fileInfo);
        }
    }
}
