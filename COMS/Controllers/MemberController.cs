using COMS.Security;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class MemberController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly ILogger _logger;

        public MemberController(IMemberService memberService, ILogger logger)
        {
            _memberService = memberService;
            _logger = logger;
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetMembers")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public List<MemberResponse> GetMembers()
        {
            return _memberService.GetMembers();
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetMember")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public MemberResponse GetMember(int Id)
        {
            return _memberService.GetMember(Id);
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("SearchMember")]
        public Page<MemberResponse> Search([FromBody] MemberSearchRequestModel request, int skip, int pageSize)
        {
            return _memberService.Search(request, skip, pageSize);
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("SaveMember")]
        public MemberResponse SaveMember([FromBody] MemberRequestModel member)
        {
            _logger.Information("Save member started");
            try
            {
                if (member.Email == null || member.Code == 0 || member.Phone == null || member.NID == 0)
                {
                    throw new BadHttpRequestException("This Email or Code or Phone invalid.");
                }

                if (_memberService.IsExistingMember(member.Email, member.Code, member.Phone, member.NID))
                {
                    throw new BadHttpRequestException("This Email or Code or Phone are already in use.");
                }

                return _memberService.SaveMember(member);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpPut("UpdateMember")]
        public ActionResult UpdateMember([FromBody] MemberRequestModel memberRequestModel)
        {
            _logger.Information($"Updating member: {memberRequestModel.Name}");
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Invalid request.");
                }

                _memberService.SaveMember(memberRequestModel);

                _logger.Information($"Successfully updated member: {memberRequestModel.Name}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [ClaimRequirement(PermissionType.Admin)]
        [HttpDelete("DeleteMember/{id}")]
        public ActionResult DeleteMember(int id)
        {
            _logger.Information($"Deleting member. id: {id}");
            
            if(id == 0)
            {
                _logger.Information("Id cannot be zero.");
                return BadRequest();
            }

            try
            {
                _logger.Information("Member successfully deleted.");
                _memberService.DeleteMember(id);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
            }
        }
    }
}
