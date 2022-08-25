using AutoMapper;
using COMS.Security;
using Core.Common;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, ILogger logger, IMapper mapper)
        {
            _transactionService = transactionService;
            _logger = logger;
            _mapper = mapper;
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetTransaction")]
        public List<TransactionResponse> GetTransactions()
        {
            _logger.Information("Get all Transactions started.");
            try
            {
                return _transactionService.GetTransactions();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetTransaction")]
        public TransactionResponse GetTransaction(int Id)
        {
            _logger.Information("Get all Transaction started.");
            try
            {
                return _transactionService.GetTransaction(Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }



        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetTransactionsByMemberId/{id}")]
        public List<TransactionResponse> GetTransactionsByMemberId(int memberId)
        {
            _logger.Information("Get all Transaction started.");
            try
            {
                return _transactionService.GetTransactionsByMemberId(memberId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetVerifiedTransactions")]
        public List<TransactionResponse> GetVerifiedTransactions()
        {
            _logger.Information("Get Verified Members started.");
            try
            {
                return _transactionService.GetVerifiedTransactions();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetRequestVerifyTransactions")]
        public List<TransactionResponse> GetRequestVerifyransactions()
        {
            _logger.Information("Get Request Verify Members started.");
            try
            {
                return _transactionService.GetRequestVerifyTransactions();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetTransactionsByProject/{id}")]
        public List<TransactionResponse> GetTransactionsByProject(int projectId)
        {
            _logger.Information("Get all Transaction started.");
            try
            {
                return _transactionService.GetTransactionsByProject(projectId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("SaveTransaction")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public TransactionResponse SaveMember([FromBody] TransactionRequest transaction)
        {
            _logger.Information("Save member started");
            try
            {
                if (transaction.MemberId == 0 || transaction.TransactionAmounts == 0
                    || transaction.TransactionType == 0 || transaction.TransactionDate == null)
                {
                    throw new BadHttpRequestException("This Email or Code or Phone invalid.");
                }

/*                if (_transactionService.IsExistingTransaction(transaction.MemberId,
                    transaction.TransactionDate, transaction.TransactionType))
                {
                    throw new BadHttpRequestException("This Transaction already exist on this month.");
                }*/

                return _transactionService.SaveTransaction(transaction);
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

            if (id == 0)
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
