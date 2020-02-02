using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GitlabInfo.Models.EFModels;

namespace GitlabInfo.Code.Repositories.Interfaces
{
    public interface IGitLabInfoDbRepository
    {
        IEnumerable<UserModel> GetUsers(Func<UserModel, bool> predicate, bool getAllProperties = false);
        GroupModel GetGroup(int gId, bool getAllProperties = false);
        JoinRequestModel GetJoinRequest(int rId);
        IEnumerable<JoinRequestModel> GetJoinRequestForGroup(int gId);
        IEnumerable<JoinRequestModel> GetJoinRequestForUser(int uId);
        IEnumerable<ProjectRequestModel> GetProjectRequests(Func<ProjectRequestModel, bool> predicate);
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entityCollection) where TEntity : class;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void AddUserWithRole(UserModel dbUser, GroupModel dbGroup, Role role);
        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TEntity : class;
        ProjectModel GetProjectWithReportedTimes(int projectId);
        void MarkNewData(int groupId, bool hasNewData = true);
        void RunMigration();
    }
}