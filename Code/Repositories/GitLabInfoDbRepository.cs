using GitlabInfo.Code.EntityFramework;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models.EFModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
                .Include(p => p.UserGroups)
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

        public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            var dbSet = _dbContext.Set<TEntity>();

            var query = dbSet.Where(predicate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var result = query.AsEnumerable();

            return result;
        }

        public ProjectModel GetProjectWithReportedTimes(int projectId)
        {
            var dbSet = _dbContext.Set<ProjectModel>();

            return dbSet.Where(g => g.Id == projectId)
                .Include(g => g.ReportedTimes)
                .ThenInclude(g => g.User)
                .FirstOrDefault();
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            SaveChanges();
        }

        public void AddUserWithRole(UserModel dbUser, GroupModel dbGroup, Role role)
        {
            var userGroup = new UserGroupModel
            {
                User = dbUser,
                Group = dbGroup,
                Role = role
            };

            dbUser.UserGroups.Add(userGroup);
            dbGroup.AssignedUsers.Add(userGroup);
            SaveChanges();
        }

        public void RunMigration()
        {
            _dbContext.Database.Migrate();
        }

        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
