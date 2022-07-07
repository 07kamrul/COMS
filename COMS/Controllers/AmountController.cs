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
    public class AmountController : BaseApiController
    {
        private readonly IAmountService _amountService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AmountController(IAmountService amountService, IMapper mapper, ILogger logger)
        {
            _amountService = amountService;
            _mapper = mapper;
            _logger = logger;   
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost]
        public ActionResult Post([FromBody] AmountRequestModel amountRequestModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Invalid request.");
                }
                else
                {
                    if(amountRequestModel.Id == 0 ||amountRequestModel.MemberId == 0 || amountRequestModel.DipositeId == 0)
                    {
                        return BadRequest();
                    }
                }
                return Ok(_amountService.SaveAmount(amountRequestModel));
            }
            catch(Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return Problem(ex.Message, null);
            }
        }

        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpGet("{id}")]
        public AmountResponse Get(int id)
        {
            return _amountService.GetAmount(id);
        }

        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpPut]
        public ActionResult UpdateAmount([FromBody] AmountRequestModel amountRequestModel)
        {
            _logger.Information($"Updating member amount: {amountRequestModel.MemberId}");
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Invalid request.");
                }

                _amountService.UpdateAmount(amountRequestModel);

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.StackTrace);
                throw;
            }
            finally
            {
                _logger.Information($"Successfully updated member:{amountRequestModel.MemberId}");
            }
        }

        [ClaimRequirement(PermissionType.Admin)]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _logger.Information($"Deleting amount id : {id}");
            
            if(id == 0)
            {
                _logger.Information("Id cannot be zero.");
                return BadRequest();
            }

            try
            {
                _amountService.DeleteAmount(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
            finally
            {
                _logger.Information("Amount successfully deleted.");
            }
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("DownloadAttachment")]
        public ActionResult DownloadAttachment(int id)
        {
            _logger.Information($"Downloading Attachment. Id: {id}");

            if(id == 0)
            {
                _logger.Information("Id cannot be zero.");
                return BadRequest("Id cannot be zero.");
            }

            try
            {
                Stream fileStream = _amountService.GetAttachmentFile(id);

                if(fileStream == null)
                {
                    return NotFound();
                }
                return File(fileStream, "application/octet-stream");
            }
            catch (FileNotFoundException fex)
            {
                _logger.Error(fex.Message);
                return Problem(fex.Message, null, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
            finally
            {
                _logger.Information("Downloading successfully downloaded.");
            }
        }

        [ClaimRequirement(PermissionType.Checker, PermissionType.Admin)]
        [HttpGet("Verify")]
        public ActionResult Verify(int amountId, bool isVerify)
        {
            _logger.Information("Verify legal case started.");

            try
            {
                _amountService.VerifyAmount(amountId, isVerify);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message, null, (int)HttpStatusCode.InternalServerError);
            }
            finally
            {
                _logger.Information("Amount case verified successfully.");
            }
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("Export")]
        public void Export([FromBody] AmountSearchRequestModel amountSearchRequestModel, int exportFormat)
        {
            List<AmountResponse> data = _amountService.Search(amountSearchRequestModel, 0, 1000).Data;
            List<AmountExportModel> exportData = new List<AmountExportModel>();
            
            foreach(var item in data)
            {
                exportData.Add(new AmountExportModel
                {
                    MemberId = item.MemberId,
                    MemberName = item.Member.Name,
                    DepositDate = item.Deposit.DepositeDate.ToString("dd/MM/yyyy"),
                    Amount = item.Amount,
                    Phone = item.Member.Phone,
                    Email = item.Member.Email,
                    JoiningDate = item.Member.JoiningDate.ToString("dd/MM/yyyy")
                });
            }

            if(exportFormat == 0)
            {
                new ExportHelper<AmountExportModel>(Response).ExportToExcel(exportData, "Amount search result.");
            }
            else if(exportFormat == 1)
            {
                new ExportHelper<AmountExportModel>(Response).ExportToPdf(exportData, "Amount search.");
            }
        }
    }

    internal class AmountExportModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string DepositDate { get; set; }
        public int Amount { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string JoiningDate { get; set; }
    }
}
