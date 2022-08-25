using AutoMapper;
using Core.Common;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : BaseApiController
    {
        private readonly Config _config;
        private readonly IProjectService _projectService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProjectController(Config config, IProjectService projectService, ILogger logger, IMapper mapper)
        {
            _config = config;
            _projectService = projectService;
            _logger = logger;
            _mapper = mapper;
        }
    }
}
