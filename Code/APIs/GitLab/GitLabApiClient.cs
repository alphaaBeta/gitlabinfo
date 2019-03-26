using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
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
        private HttpClient HttpClient { get; set; }

        public GitLabApiClient(
            IHttpContextAccessor httpContextAccessor, 
            IHttpClientFactory httpClientFactory)
        {
            HttpContext = httpContextAccessor.HttpContext;
            HttpClient = httpClientFactory.CreateClient(_httpClientName);
        }

        public async Task<T> GETAsync<T>(string relativeUrl) where T : class
        {
            var token = HttpContext.GetTokenAsync(_authSchema, _accessTokenPropertyName);
            var tokenType = HttpContext.GetTokenAsync(_authSchema, _tokenTypePropertyName);

            var serializer = new DataContractJsonSerializer(typeof(T));

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CultureInfo.CurrentCulture.TextInfo.ToTitleCase((await tokenType).ToLower()), await token);

            var response = HttpClient.GetStreamAsync(relativeUrl.TrimStart('/'));

            return serializer.ReadObject(await response) as T;
        }
        public async Task<T> POSTAsync<T>(string relativeUrl, object content) where T : class
        {
            var token = HttpContext.GetTokenAsync(_authSchema, _accessTokenPropertyName);
            var tokenType = HttpContext.GetTokenAsync(_authSchema, _tokenTypePropertyName);

            var serializer = new DataContractJsonSerializer(typeof(T));

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CultureInfo.CurrentCulture.TextInfo.ToTitleCase((await tokenType).ToLower()), await token);

            var response = await HttpClient.PostAsJsonAsync(relativeUrl.TrimStart('/'), content);
            response.EnsureSuccessStatusCode();

            return serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as T;
        }
    }
}
