using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GitlabInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleDataController : Controller
    {
        private readonly ILogger _logger;
        private readonly GitLabGroupRepository _groupRepository;

        public SampleDataController(ILogger<SampleDataController> logger, GitLabGroupRepository groupRepository)
        {
            _logger = logger;
            _groupRepository = groupRepository;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        [Authorize]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var group = _groupRepository.GetRootGroupByName("gitlabinfotest");
            var rng = new Random();
            _logger.LogInformation("Randomizing data");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
