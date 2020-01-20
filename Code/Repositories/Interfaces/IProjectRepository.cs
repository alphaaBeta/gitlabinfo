using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitlabInfo.Code.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> CreateProjectFromRequest(ProjectRequestModel requestModel);
        Task<Project> GetProjectDetails(int projectId);
        Task<IEnumerable<User>> GetMembers(int projectId, bool getAll = false);
    }
}
