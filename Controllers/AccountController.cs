using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GitlabInfo.Code.EntiyFramework;
using GitlabInfo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GitlabInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private GitLabInfoDbContext _dbContext { get; set; }
        public AccountController(GitLabInfoDbContext dbContext)
        {
            _dbContext = dbContext;
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
        [Authorize]
        public User Index()
        {
            var gitLabUser = new User(User);
            var us = new User(gitLabUser.GitLabId, DateTime.MaxValue, DateTime.MinValue);
            
            if (_dbContext.Users.Any(e => e.GitLabId == us.GitLabId))
                _dbContext.Update(us);
            else
                _dbContext.Add(us);

            _dbContext.SaveChanges();
            return gitLabUser;
        }
    }
}