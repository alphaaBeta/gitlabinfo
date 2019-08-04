using System;
using System.Collections.Generic;
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
        void RemoveRange<TEntity>(Func<TEntity, bool> predicate) where TEntity : class;
        void AddUserAsOwner(UserModel dbUser, GroupModel dbGroup, Role role);
    }
}