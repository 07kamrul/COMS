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
        [HttpGet("Member")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public List<MemberResponse> GetMembers()
        {
            return _memberService.GetMembers();
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("Member")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public MemberResponse GetMember(int Id)
        {
            return _memberService.GetMember(Id);
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("Search")]
        public Page<MemberResponse> Search([FromBody] MemberSearchRequestModel request, int skip, int pageSize)
        {
            return _memberService.Search(request, skip, pageSize);
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("Member")]
        public MemberResponse SaveMember([FromBody] MemberRequestModel memberRequestModel)
        {
            return _memberService.SaveMember(memberRequestModel);
        }

        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpPut]
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
        [HttpDelete("Member/{id}")]
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
