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
using System.Net;
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


        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpPost("SaveProject")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ProjectResponse SaveProject([FromBody] ProjectRequest project)
        {
            _logger.Information("Save Account started");
            try
            {
                if (project.StartDate == null || project.EndDate == null || project.NumberOfShare == 0)
                {
                    throw new BadHttpRequestException("This Member or Project or Payable amount are invalid.");
                }

                return _projectService.SaveProject(project);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }


        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpPut("UpdateProject")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult UpdateAccount([FromBody] ProjectRequest project)
        {
            _logger.Information($"Updating Account: {project.ProjectName}");
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("Invalid request.");
                }

                _projectService.SaveProject(project);

                _logger.Information($"Successfully updated project: {project.ProjectName}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [ClaimRequirement(PermissionType.Admin)]
        [HttpDelete("DeleteProject/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult DeleteProject(int id)
        {
            _logger.Information($"Deleting Project. id: {id}");

            if (id == 0)
            {
                _logger.Information("Id cannot be zero.");
                return BadRequest();
            }

            try
            {
                _logger.Information("Account successfully deleted.");
                _projectService.DeleteProject(id);
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
