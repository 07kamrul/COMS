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
                Name = ((AssemblyProductAttribute) System.Attribute
                    .GetCustomAttributes(GetType().Assembly, true)[10]).Product,

                Company = ((AssemblyCompanyAttribute)System.Attribute
                    .GetCustomAttributes(GetType().Assembly)[6]).Company,

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
        [HttpGet("Member")]
        public MemberResponse GetMember(int memberId) => _metadataService.GetMember(memberId);


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Projects")]
        public List<ProjectResponse> GetProjects() => _metadataService.GetProjects();


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Project")]
        public ProjectResponse GetProject(int projectId) => _metadataService.GetProject(projectId);


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Transaction")]
        public TransactionResponse GetTransaction(int transactionId) => _metadataService.GetTransaction(transactionId);


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("Transactions")]
        public List<TransactionResponse> GetTransactions() => _metadataService.GetTransactions();


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("TransactionsByMemberId")]
        public List<TransactionResponse> GetTransactionsByMemberId(int memberId) => _metadataService.GetTransactionsByMemberId(memberId);


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [HttpGet("ProjectsByMemberId")]
        public List<MemberResponse> GetProjectsByMemberId(int memberId) => _metadataService.GetProjectsByMemberId(memberId);

    }
}
