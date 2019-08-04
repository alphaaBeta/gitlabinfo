using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models;

namespace GitlabInfo.Code.GitLabApis
{
    public interface IStandaloneApiClient
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> TryGetUserByEmail(string userEmail);
    }
}
