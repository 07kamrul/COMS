using COMS.Security;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public AccountController(IAccountService accountService, ILogger logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetAccount")]
        public AccountResponse GetAccount(int Id)
        {
            _logger.Information("Get all Account started.");
            try
            {
                return _accountService.GetAccount(Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetAccounts")]
        public List<AccountResponse> GetAccounts()
        {
            _logger.Information("Get all Accounts started.");
            try
            {
                return _accountService.GetAccounts();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetAccountsByMember/{id}")]
        public List<AccountResponse> GetAccountsByMember(int id)
        {
            _logger.Information("Get all Accounts started.");
            try
            {
                return _accountService.GetAccountsByMember(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetAccountsByProject/{id}")]
        public List<AccountResponse> GetAccountsByProject(int id)
        {
            _logger.Information("Get all Accounts started.");
            try
            {
                return _accountService.GetAccountsByProject(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetInactiveAccounts")]
        public List<AccountResponse> GetInactiveAccounts()
        {
            _logger.Information("Get inactive Accounts started.");
            try
            {
                return _accountService.GetInactiveAccounts();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetVerifiedAccounts")]
        public List<AccountResponse> GetVerifiedAccounts()
        {
            _logger.Information("Get Verified Accounts started.");
            try
            {
                return _accountService.GetVerifiedAccounts();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetRequestVerifyAccounts")]
        public List<AccountResponse> GetRequestVerifyAccounts()
        {
            _logger.Information("Get Request Verify Accounts started.");
            try
            {
                return _accountService.GetRequestVerifyAccounts();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("SaveAccount")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public AccountResponse SaveAccount([FromBody] AccountRequest account)
        {
            _logger.Information("Save Account started");
            try
            {
                if (account.MemberId == 0 || account.ProjectId == 0 || account.PayableAmounts == 0)
                {
                    throw new BadHttpRequestException("This Member or Project or Payable amount are invalid.");
                }

                return _accountService.SaveAccount(account);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpPut("UpdateAccount")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult UpdateAccount([FromBody] AccountRequest account)
        {
            _logger.Information($"Updating Account: {account.MemberId}");
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Invalid request.");
                }

                _accountService.SaveAccount(account);

                _logger.Information($"Successfully updated Account: {account.MemberId}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [ClaimRequirement(PermissionType.Admin)]
        [HttpDelete("DeleteAccount/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult DeleteAccount(int id)
        {
            _logger.Information($"Deleting Account. id: {id}");

            if (id == 0)
            {
                _logger.Information("Id cannot be zero.");
                return BadRequest();
            }

            try
            {
                _logger.Information("Account successfully deleted.");
                _accountService.DeleteAccount(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
