using COMS.Security;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AttachmentController : Controller
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("Attachment")]
        public ActionResult SaveAttachment([FromForm] AttachmentRequest attachmentRequestModel)
        {
            if (attachmentRequestModel.AttachmentTypeId == 0 || attachmentRequestModel.MemberId == 0 || attachmentRequestModel.File.Length == 0)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_attachmentService.SaveAttachment(attachmentRequestModel));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("Attachments")]
        public List<AttachmentResponse> GetAttachments()
        {
            return _attachmentService.GetAttachments();
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("Attachment/{memberId}")]
        public List<AttachmentResponse> GetAttachmentByMemberId(int memberId)
        {
            return _attachmentService.GetAttachmentByMemberId(memberId);
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("Attachment/{id}")]
        public AttachmentResponse GetAttachment(int id)
        {
            return _attachmentService.GetAttachment(id);
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("DownloadAttachment")]
        public ActionResult DownloadAttachment(int id)
        {
            if(id == 0)
            {
                return BadRequest("Id cannot be zero");
            }

            try
            {
                Stream fileStream = _attachmentService.GetAttachmentFile(id);
                if(fileStream == null)
                {
                    return NotFound();
                }
                return File(fileStream, "application/octet-stream");
            }
            catch (FileNotFoundException fex)
            {
                //_logger.Error(fex.Message);
                return Problem(fex.Message, null, (int)HttpStatusCode.NotFound);
            }
            catch(Exception ex)
            {
                //_logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
            finally
            {
                //_logger.Information("Downloading successfully downloaded");
            }
        }

        [ClaimRequirement(PermissionType.Admin)]
        [HttpDelete("Attachment/{attachmentId}")]
        public ActionResult DeleteAttachment(int attachmentId)
        {
            if(attachmentId == 0)
            {
                // _logger.Information("Id cannot be zero");
                return BadRequest("Id cannot be zero");
            }

            try
            {
                _attachmentService.DeleteAttachment(attachmentId);
                return Ok();
            }
            catch(Exception ex)
            {   
                //_logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
            finally
            {
                //_logger.Information("Attachment successfully deleted");
            }
        }
    }
}
