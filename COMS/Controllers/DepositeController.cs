using AutoMapper;
using COMS.Helper;
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
using System.IO;
using System.Net;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DepositeController : BaseApiController
    {
        private readonly Config _config;
        private readonly ILogger _logger;
        private readonly IDepositService _depositService;

        public DepositeController(Config config, ILogger logger, IDepositService depositService)
        {
            _config = config;
            _logger = logger;
            _depositService = depositService;
        }

        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpGet("GetDeposit/{id}")]
        public DepositResponse GetDeposite(int id)
        {
            _logger.Information("Get Deposite Started.");
            try
            {
                return _depositService.GetDeposit(id);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetDeposits")]
        public List<DepositResponse> GetDeposits()
        {
            _logger.Information("Get all deposites started.");
            try
            {
                return _depositService.GetDeposits();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetDepositsByMemberId/{id}")]
        public List<DepositResponse> GetDepositsByMemberId(int memberId)
        {
            _logger.Information("Get all deposites started.");
            try
            {
                return _depositService.GetDepositsByMemberId(memberId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("SaveDeposit")]
        public ActionResult SaveDeposit([FromBody] DepositRequestModel deposit)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Invalid request.");
                }
            }
        }
    }
}
