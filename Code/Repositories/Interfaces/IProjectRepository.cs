using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;

namespace GitlabInfo.Code.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> CreateProjectFromRequest(ProjectRequestModel requestModel);
        Project GetProjectDetails(int projectId);
        IEnumerable<User> GetMembers(int projectId);
    }
}
