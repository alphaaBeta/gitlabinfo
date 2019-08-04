using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Code.EntiyFramework;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GitlabInfo.Code.Repositories
{
    public class GitLabInfoDbRepository : IGitLabInfoDbRepository
    {
        private readonly ILogger _logger;
        private GitLabInfoDbContext _dbContext { get; set; }
        public GitLabInfoDbRepository(ILogger<GitLabInfoDbRepository> logger, GitLabInfoDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IEnumerable<UserModel> GetUsers(Func<UserModel, bool> predicate, bool getAllProperties = false)
        {
            if (!getAllProperties)
                return _dbContext.Users.Where(predicate);

            return _dbContext.Users
                .Include(p => p.OwnedGroups)
                .Where(predicate);
        }

        public GroupModel GetGroup(int gId, bool getAllProperties = false)
        {
            if (!getAllProperties)
            {
                return _dbContext.Groups.Find(gId);
            }

            return _dbContext.Groups
                .Include(x => x.AssignedUsers)
                .Include(x => x.Projects)
                .FirstOrDefault(g => g.Id == gId);
        }

        public JoinRequestModel GetJoinRequest(int rId)
        {
            return _dbContext.JoinRequests
                .Include(x => x.RequestedGroup)
                .Include(x => x.Requestee)
                .FirstOrDefault(r => r.Id == rId);
        }

        public IEnumerable<JoinRequestModel> GetJoinRequestForGroup(int gId)
        {
            return _dbContext.JoinRequests
                .Include(x => x.RequestedGroup)
                .Include(x => x.Requestee)
                .Where(r => r.RequestedGroup.Id == gId);
        }

        public IEnumerable<JoinRequestModel> GetJoinRequestForUser(int uId)
        {
            return _dbContext.JoinRequests
                .Include(x => x.RequestedGroup)
                .Include(x => x.Requestee)
                .Where(r => r.Requestee.Id == uId);
        }

        public IEnumerable<ProjectRequestModel> GetProjectRequests(Func<ProjectRequestModel, bool> predicate)
        {
            return _dbContext.ProjectRequests
                .Include(x => x.Members)
                .Include(x => x.Requestee)
                .Include(x => x.ParentGroup)
                .Where(predicate);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
            SaveChanges();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
            SaveChanges();
        }
        public void AddRange<TEntity>(IEnumerable<TEntity> entityCollection) where TEntity : class
        {
            _dbContext.Set<TEntity>().AddRange(entityCollection);
            SaveChanges();
        }

        public void RemoveRange<TEntity>(Func<TEntity,bool> predicate) where TEntity : class
        {
            var entities = _dbContext.Set<TEntity>().Where(predicate);
            _dbContext.Set<TEntity>().RemoveRange(entities);
            SaveChanges();
        }

        public void AddUserAsOwner(UserModel dbUser, GroupModel dbGroup, Role role)
        {
            var userGroup = new UserGroupModel
            {
                User = dbUser,
                Group = dbGroup,
                Role = role
            };

            dbUser.OwnedGroups.Add(userGroup);
            dbGroup.AssignedUsers.Add(userGroup);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
