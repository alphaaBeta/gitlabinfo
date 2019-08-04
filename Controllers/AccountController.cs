using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GitlabInfo.Code.EntiyFramework;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GitlabInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IGitLabInfoDbRepository _DbRepository { get; set; }
        public AccountController(IGitLabInfoDbRepository dbRepository)
        {
            _DbRepository = dbRepository;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/account")
        {
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl });
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect(Url.Content("~/"));
        }

        [HttpGet]
        [AllowAnonymous]
        public User Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            var gitLabUser = new User(User);
            var dbUser = _DbRepository.GetUsers(user => user.Id == gitLabUser.Id).FirstOrDefault();

            if (dbUser == null)
            {
                _DbRepository.Add(new UserModel(gitLabUser.Id, gitLabUser.Email, DateTime.UtcNow, DateTime.UtcNow));
            }
            else
            {
                dbUser.LastJoined = DateTime.UtcNow;
                if (dbUser.Email is null)
                    dbUser.Email = gitLabUser.Email;

                _DbRepository.Update(dbUser);
            }

            return gitLabUser;
        }
    }
}