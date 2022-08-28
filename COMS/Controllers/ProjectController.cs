using AutoMapper;
using COMS.Security;
using Core.Common;
using Core.Service;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : BaseApiController
    {
        private readonly Config _config;
        private readonly IProjectService _projectService;
        private readonly ILogger _logger;

        public ProjectController(Config config, IProjectService projectService, ILogger logger)
        {
            _config = config;
            _projectService = projectService;
            _logger = logger;
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetProjects")]
        public List<ProjectResponse> GetProjects()
        {
            _logger.Information("Get all Projects started.");
            try
            {
                return _projectService.GetProjects();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("GetProject/{id}")]
        public ProjectResponse GetProject(int Id)
        {
            _logger.Information("Get all Project started.");
            try
            {
                return _projectService.GetProject(Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }




    }
}
