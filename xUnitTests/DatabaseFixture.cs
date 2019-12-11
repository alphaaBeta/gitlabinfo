using System;
using System.Collections.Generic;
using GitlabInfo.Code.EntityFramework;
using GitlabInfo.Models.EFModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace xUnitTests
{
    public class DatabaseFixture : IDisposable
    {
        public SqliteConnection Db { get; private set; }

        public DatabaseFixture()
        {
            Db = new SqliteConnection("DataSource=:memory:");
            Db.Open();

            try
            {
                var options = new DbContextOptionsBuilder<GitLabInfoDbContext>()
                    .UseSqlite(Db)
                    .Options;

                using (var context = new GitLabInfoDbContext(options))
                {
                    context.Database.EnsureCreated();
                    PrepareScenarioAsync(context);
                }
            }
            catch
            {
                Db.Close();
                throw;
            }
        }

        public void Dispose()
        {
            Db.Close();
        }

        public GitLabInfoDbContext GetGitLabInfoDbContext()
        {
            var options = new DbContextOptionsBuilder<GitLabInfoDbContext>()
                .UseSqlite(Db)
                .Options;

            return new GitLabInfoDbContext(options);
        }

        private void PrepareScenarioAsync(GitLabInfoDbContext context)
        {
            var userModels = new List<UserModel>
            {
                new UserModel()
                {
                    Id = 1,
                    FirstJoined = DateTime.MaxValue,
                    LastJoined = DateTime.MaxValue,
                    UserGroups = new List<UserGroupModel>()
                },
                new UserModel()
                {
                    Id = 2,
                    FirstJoined = DateTime.MaxValue,
                    LastJoined = DateTime.MaxValue,
                    UserGroups = new List<UserGroupModel>()
                },
                new UserModel()
                {
                    Id = 3,
                    FirstJoined = DateTime.MaxValue,
                    LastJoined = DateTime.MaxValue,
                    UserGroups = new List<UserGroupModel>()
                }
            };

            var groupModels = new List<GroupModel>
            {
                new GroupModel()
                {
                    Id = 10,
                    AssignedUsers = new List<UserGroupModel>()
                },
                new GroupModel()
                {
                    Id = 11,
                    AssignedUsers = new List<UserGroupModel>()
                },
                new GroupModel()
                {
                    Id = 12,
                    AssignedUsers = new List<UserGroupModel>()
                }
            };

            var userGroupModels = new List<UserGroupModel>
            {
                new UserGroupModel()
                {
                    Group = groupModels[0],
                    User = userModels[0],
                    Role = Role.Owner
                },
                new UserGroupModel()
                {
                    Group = groupModels[1],
                    User = userModels[1],
                    Role = Role.Owner
                }
            };
            userModels[0].UserGroups.Add(userGroupModels[0]);
            groupModels[0].AssignedUsers.Add(userGroupModels[0]);

            userModels[1].UserGroups.Add(userGroupModels[1]);
            groupModels[1].AssignedUsers.Add(userGroupModels[1]);

            var joinRequestModels = new List<JoinRequestModel>
            {
                new JoinRequestModel()
                {
                    Id = 1,
                    RequestedGroup = groupModels[0],
                    Requestee = userModels[0]
                }
            };

            context.Users.AddRange(userModels);
            context.Groups.AddRange(groupModels);
            context.JoinRequests.AddRange(joinRequestModels);

            context.SaveChanges();
        }
    }
}
