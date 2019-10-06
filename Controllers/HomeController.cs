using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Code.EntityFramework;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GitlabInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;

        HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
    }
}