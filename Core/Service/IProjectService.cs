using Core.RequestModels;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProjectService
    {
        List<ProjectResponse> GetProjects();
        ProjectResponse GetProject(int id);
        ProjectResponse SaveProject(ProjectRequest project);
        void DeleteProject(int id);
        List<ProjectResponse> GetActiveProjects();
        List<ProjectResponse> GetInActiveProjects();
    }
}
