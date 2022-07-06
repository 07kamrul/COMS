using COMS.Security;
using Core.Service;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class MetadataController : Controller
    {
        private readonly IMetadataService _metadataService;

        public MetadataController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        [AllowAnonymous]
        [HttpGet("Info")]
        public dynamic GetAppInfo()
        {
            return JsonConvert.SerializeObject(new
            {
                Name = ((AssemblyProductAttribute) System.Attribute.GetCustomAttributes(GetType().Assembly, true)[10]).Product,
                Company = ((AssemblyCompanyAttribute)System.Attribute.GetCustomAttributes(GetType().Assembly)[6]).Company,
                Version = GetType().Assembly.GetName().Version.ToString(),
            });
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("AttachmentTypes")]
        public List<AttachmentTypeResponse> GetAttachmentTypes() => _metadataService.GetAttachmentTypes();

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Members")]
        public List<MemberResponse> GetMembers() => _metadataService.GetMembers();

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Amounts")]
        public List<AmountResponse> GetAmounts() => _metadataService.GetAmounts();

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Amount")]
        public AmountResponse GetAmount(int amountId) => _metadataService.GetAmount(amountId);

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Deposit")]
        public DepositResponse GetDeposit(int depositId) => _metadataService.GetDeposit(depositId);

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Deposit")]
        public List<DepositResponse> GetDeposits() => _metadataService.GetDeposits();

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Member")]
        public MemberResponse GetMember(int memberId) => _metadataService.GetMember(memberId);

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Deposit")]
        public List<DepositResponse> GetDepositsByMemberId(int memberId) => _metadataService.GetDepositsByMemberId(memberId);

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Amount")]
        public List<AmountResponse> GetAmountsByMemberId(int memberId) => _metadataService.GetAmountsByMemberId(memberId);

    }
}
