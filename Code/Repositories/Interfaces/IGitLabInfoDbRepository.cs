using System.Collections.Generic;
using GitlabInfo.Models.EFModels;

namespace GitlabInfo.Code.Repositories.Interfaces
{
    public interface IGitLabInfoDbRepository
    {
        UserModel GetUser(int uId, bool getAllProperties = false);
        GroupModel GetGroup(int gId, bool getAllProperties = false);
        JoinRequestModel GetJoinRequest(int rId);
        IEnumerable<JoinRequestModel> GetJoinRequestForGroup(int gId);
        IEnumerable<JoinRequestModel> GetJoinRequestForUser(int uId);
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entityCollection) where TEntity : class;
        void AddUserAsOwner(UserModel dbUser, GroupModel dbGroup, Role role);
    }
}