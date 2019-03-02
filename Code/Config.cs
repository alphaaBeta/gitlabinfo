using System;
using System.Runtime.CompilerServices;
using GitlabInfo.Code.Exceptions;
using Microsoft.Extensions.Configuration;

namespace GitlabInfo.Code
{
    public class Config
    {
        private static IConfiguration _configuration;
        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GitLab_ClientId => GetConfig("", "GitLab:ClientId");
        public static string GitLab_ClientSecret => GetConfig("", "GitLab:ClientSecret");
        public static string GitLab_RedirectUrl => GetConfig("", "GitLab:RedirectUrl");

        /// <summary>
        /// Gets the value for key (CallerMemberName).
        /// </summary>
        /// <param name="defaultValue">Default value to be returned. Exception will not be thrown if supplied</param>
        /// <param name="key">Key of the setting, defaults to CallerMemberName</param>
        /// <returns>The value of config setting</returns>
        /// <exception cref="ConfigurationException">Value is null or missing from config and default value is not set</exception>
        private static string GetConfig(string defaultValue = "", [CallerMemberName] string key = "")
        {
            var configValue = _configuration[key];

            var value = string.IsNullOrEmpty(configValue) ? defaultValue : configValue;

            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationException($"Value for key {key} or the key itself is missing");
            }

            return value;
        }
    }
}