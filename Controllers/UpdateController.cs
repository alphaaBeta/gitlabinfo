using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Code;
using GitlabInfo.Code.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GitlabInfo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private IGitLabInfoDbRepository _DbRepository { get; set; }
        public UpdateController(IGitLabInfoDbRepository dbRepository)
        {
            _DbRepository = dbRepository;
        }
        [HttpGet]
        public IActionResult Database(string accesskey)
        {
            if (accesskey == Config.Database_EFUpdateKey)
            {
                _DbRepository.RunMigration();
                return new OkResult();
            }
            return new ForbidResult();
        }
    }
}