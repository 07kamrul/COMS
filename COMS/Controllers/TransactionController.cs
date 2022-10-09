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
using Service;
using System;
using System.Collections.Generic;
using System.Net;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger _logger;

        public TransactionController(ITransactionService transactionService, ILogger logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetTransactions")]
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
        [HttpGet("GetTransaction/{id}")]
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
        public List<TransactionResponse> GetRequestVerifyTransactions()
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
        public ActionResult SaveTransaction([FromBody] TransactionRequest transaction)
        {
            _logger.Information("Save member started");
            try
            {
                if (transaction.MemberId == 0 || transaction.AccountId == 0
                    || transaction.TransactionAmounts == 0 || transaction.TransactionType == 0)
                {
                    throw new BadHttpRequestException("This MemberId or AccountId or TransactionAmounts or TransactionType invalid.");
                }

                _transactionService.SaveTransaction(transaction);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }


        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpPut("UpdateTransaction")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult UpdateTransaction([FromBody] TransactionRequest transaction)
        {
            _logger.Information($"Updating member: {transaction.TransactionDate}");
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Invalid request.");
                }

                _transactionService.SaveTransaction(transaction);

                _logger.Information($"Successfully updated member: {transaction.TransactionDate}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [ClaimRequirement(PermissionType.Admin)]
        [HttpDelete("DeleteTransaction/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult DeleteTransaction(int id)
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
                _transactionService.DeleteTransaction(id);
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
