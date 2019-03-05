using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Code.GitLabApis
{
    interface IApiClient
    {
        Task<T> GETAsync<T>(string relativeUrl) where T:class ;
    }
}
