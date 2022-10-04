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
using System.Security.Principal;
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
        [HttpGet("GetMember/{id}")]
        public MemberResponse GetMember(int Id)
        {
            _logger.Information("Get all member started.");
            try
            {
                return _memberService.GetMember(Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetMembers")]
        public List<MemberResponse> GetMembers()
        {
            _logger.Information("Get all members started.");
            try
            {
                return _memberService.GetMembers();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetInactiveMembers")]
        public List<MemberResponse> GetInactiveMembers()
        {
            _logger.Information("Get inactive members started.");
            try
            {
                return _memberService.GetInactiveMembers();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetActiveMembers")]
        public List<MemberResponse> GetActiveMembers()
        {
            _logger.Information("Get inactive members started.");
            try
            {
                return _memberService.GetActiveMembers();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetVerifiedMembers")]
        public List<MemberResponse> GetVerifiedMembers()
        {
            _logger.Information("Get Verified Members started.");
            try
            {
                return _memberService.GetVerifiedMembers();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetRequestVerifyMembers")]
        public List<MemberResponse> GetRequestVerifyMembers()
        {
            _logger.Information("Get Request Verify Members started.");
            try
            {
                return _memberService.GetRequestVerifyMembers();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("SearchMember")]
        public Page<MemberResponse> Search([FromBody] MemberSearchRequest request, int skip, int pageSize)
        {
            _logger.Information("Search member started.");
            try
            {
                return _memberService.Search(request, skip, pageSize);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("SaveMember")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public MemberResponse SaveMember([FromBody] MemberRequest member)
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


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("VerifyMember")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public MemberResponse VerifyMember([FromBody] MemberVerifyRequest member)
        {
            _logger.Information("Verify member started");
            try
            {
                if (member.MemberId == 0)
                {
                    throw new BadHttpRequestException("This member invalid.");
                }

                return _memberService.VerifyMember(member);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpPut("UpdateMember")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult UpdateMember([FromBody] MemberRequest memberRequestModel)
        {
            _logger.Information($"Updating member: {memberRequestModel.Name}");
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Invalid request.");
                }

                if (memberRequestModel.Id == 0)
                {
                    throw new BadHttpRequestException("Invalid member id.");
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
        [ApiExplorerSettings(IgnoreApi = true)]
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
