using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models;

namespace GitlabInfo.Code.GitLabApis
{

    public interface IProjectApiClient
    {
        Task<IEnumerable<User>> GetMembersByProjectId(int projectId);
        Task<IEnumerable<Issue>> GetIssuesByProjectIdAndLabel(int projectId, IEnumerable<string> labels = null);
        Task<Project> CreateProject(Project projectModel);
        Task<Issue> CreateIssue(int projectId, Issue issueModel);
        Task<User> AddUserToProject(int projectId, int userId, int accessLevel);
    }
}
