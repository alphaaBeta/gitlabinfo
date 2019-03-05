using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using GitlabInfo.Code.GitLabApis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace GitlabInfo.Code.APIs.GitLab
{
    public class GitLabApiClient : IApiClient
    {
        private readonly string _httpClientName = "GitLabApi";
        private readonly string _authSchema = "GitLab";
        private readonly string _accessTokenPropertyName = "access_token";
        private readonly string _tokenTypePropertyName = "token_type";
        private HttpContext HttpContext { get; set; }
        private IHttpClientFactory HttpClientFactory { get; set; }

        public GitLabApiClient(
            IHttpContextAccessor httpContextAccessor, 
            IHttpClientFactory httpClientFactory)
        {
            HttpContext = httpContextAccessor.HttpContext;
            HttpClientFactory = httpClientFactory;
        }

        public async Task<T> GETAsync<T>(string relativeUrl) where T : class
        {
            var token = HttpContext.GetTokenAsync(_authSchema, _accessTokenPropertyName);
            var tokenType = HttpContext.GetTokenAsync(_authSchema, _tokenTypePropertyName);

            var serializer = new DataContractJsonSerializer(typeof(T));

            var client = HttpClientFactory.CreateClient(_httpClientName);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(await tokenType, await token);

            var response = client.GetStreamAsync(relativeUrl.TrimStart('/'));

            return serializer.ReadObject(await response) as T;
        }
    }
}
