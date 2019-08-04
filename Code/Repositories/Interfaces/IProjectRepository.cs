using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models.EFModels;

namespace GitlabInfo.Code.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        void CreateProjectFromRequest(ProjectRequestModel requestModel)
    }
}
