using AutoMapper;
using Core.FileStore;
using Core.Repository;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileStore _fileStore;
        private readonly IConfiguration _configuration;

        public ProjectService(IMapper mapper, IProjectRepository projectRepository,
            IAttachmentRepository attachmentRepository, IFileStore fileStore, IConfiguration configuration)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _attachmentRepository = attachmentRepository;
            _fileStore = fileStore;
            _configuration = configuration;
        }

        public ProjectResponse GetProject(int id)
        {
            return _mapper.Map<ProjectResponse>(_projectRepository.GetById(id));
        }


        public List<ProjectResponse> GetProjects()
        {
            return _mapper.Map<List<ProjectResponse>>(_projectRepository.GetAll());

        }

        public ProjectResponse SaveProject(ProjectRequest projectModel)
        {
            Project existingMember = _projectRepository.FindBy(x => x.StartDate == projectModel.StartDate
                || x.EndDate == projectModel.EndDate).FirstOrDefault();

            Project project = _mapper.Map<Project>(projectModel);
            project.StartDate = projectModel.StartDate;
            project.EndDate = projectModel.EndDate;
            project.IsActive = true;

            if (existingMember == null)
            {
                project.Id = 0;
                return _mapper.Map<ProjectResponse>(_projectRepository.Add(project));
            }

            project.Id = existingMember.Id;
            return _mapper.Map<ProjectResponse>(project);
        }

        public void DeleteProject(int id)
        {
            _projectRepository.Delete(_projectRepository.GetById(id));
        }
    }
}
