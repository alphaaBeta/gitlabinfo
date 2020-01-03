using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GitlabInfo.Code.EntityFramework;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Controllers;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using GitlabInfo.Models.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using xUnitTests;
using Xunit;
using GitlabInfo.Models.ViewModel;

namespace XUnitTests
{
    [Collection("Database collection")]
    public class GroupTests
    {
        private IGroupApiClient _groupApiClient;
        private IProjectApiClient _projectApiClient;
        private IStandaloneApiClient _standaloneApiClient;
        private DatabaseFixture _dbFixture;


        public GroupTests(DatabaseFixture dbFixture)
        {
            _dbFixture = dbFixture;
            _groupApiClient = PrepareMockGroupApiClient();
            _projectApiClient = PrepareMockProjectApiClient();
            _standaloneApiClient = PrepareMockStandaloneApiClient();
        }

        [Fact]
        public void RequestToJoinGroup_NotInGroup_Ok()
        {
            var groupController = PrepareGroupController(2);

            var result = groupController.RequestToJoinGroup(11);

            Assert.IsType<OkResult>(result);
            Assert.Contains(groupController.DbRepository.GetJoinRequestForUser(2), x => x.Requestee.Id == 2);
        }

        [Fact]
        public void RequestToJoinGroup_UserAlreadyInGroup_Conflict()
        {
            var groupController = PrepareGroupController(3);

            var result = groupController.RequestToJoinGroup(11);

            Assert.IsType<ConflictObjectResult>(result);
            Assert.DoesNotContain(groupController.DbRepository.GetJoinRequestForUser(3), x => x.Requestee.Id == 3);
        }

        [Fact]
        public void RequestToJoinGroup_NotInGroupAndDatabase_NotFound()
        {
            var groupController = PrepareGroupController(4);

            var result = groupController.RequestToJoinGroup(10);

            Assert.IsType<NotFoundResult>(result);
            Assert.DoesNotContain(groupController.DbRepository.GetJoinRequestForUser(4), x => x.Requestee.Id == 4);
        }

        [Fact]
        public async Task GetOwnedGroups_UserNotSupplied_GroupsForCurrentUserReturnedAsync()
        {
            var groupController = PrepareGroupController(1);

            var result = (await groupController.GetGroups(null, 50)).Value;

            Assert.IsType<List<GroupDto>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetOwnedGroups_UserSupplied_GroupsForSuppliedUserReturnedAsync()
        {
            var groupController = PrepareGroupController(2);

            var result = (await groupController.GetGroups(1, 50)).Value;

            Assert.IsType<List<GroupDto>>(result);
            Assert.Single(result);
        }

        [Fact]
        public void AddCurrentUserAsGroupOwner_UserHasAccess_Ok()
        {
            var groupController = PrepareGroupController(1);

            var result = groupController.AddCurrentUserAsGroupOwner(12);

            Assert.IsType<OkResult>(result);
            Assert.Contains(groupController.DbRepository.GetUsers(user => user.Id == 1, true).First().UserGroups,
                x => x.GroupId == 12 && x.Role >= Role.Maintainer);
        }

        [Fact]
        public void AddCurrentUserAsGroupOwner_UserNotAuthorized_Unauthorized()
        {
            var groupController = PrepareGroupController(2);

            var result = groupController.AddCurrentUserAsGroupOwner(12);

            Assert.IsType<UnauthorizedResult>(result);
            Assert.DoesNotContain(groupController.DbRepository.GetUsers(user => user.Id == 2, true).First().UserGroups,
                x => x.GroupId == 12 );
        }

        private IGroupApiClient PrepareMockGroupApiClient()
        {
            var groupApiClientMock = new Mock<IGroupApiClient>();

            groupApiClientMock
                .Setup(service => service.GetRootGroupByNameAsync("TestGroup10"))
                .Returns<string>(name => Task.FromResult(new Group()
                {
                    Id = 10,
                    Name = name
                }));

            groupApiClientMock
                .Setup(service => service.GetGroupByIdAsync(It.IsAny<int>()))
                .Returns<int>(gid => Task.FromResult(new Group()
                {
                    Id = gid
                }));

            groupApiClientMock
                .Setup(service => service.GetSubGroupsByGroupIdAsync(10))
                .Returns(Task.FromResult(new List<Group>(){
                    new Group(){
                        Id = 11,
                    },
                    new Group(){
                        Id = 12,
                    },
                    new Group(){
                        Id = 13,
                    }
                }.AsEnumerable()));

            groupApiClientMock
                .Setup(service => service.GetProjectsByGroupIdAsync(10))
                .Returns(Task.FromResult(new List<Project>()
                {
                    new Project()
                    {
                        Id = 21
                    },
                    new Project()
                    {
                        Id = 22
                    },
                    new Project()
                    {
                        Id = 23
                    }
                }.AsEnumerable()));

            groupApiClientMock
                .Setup(service => service.GetMembersByGroupIdAsync(10))
                .Returns(Task.FromResult(new List<User>()
                {
                    new User()
                    {
                        Id = 1
                    },
                    new User()
                    {
                        Id = 2
                    },
                    new User()
                    {
                        Id = 3
                    }
                }.AsEnumerable()));
            groupApiClientMock
                .Setup(service => service.GetMembersByGroupIdAsync(12))
                .Returns(Task.FromResult(new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        AccessLevel = 50
                    },
                    new User()
                    {
                        Id = 2,
                        AccessLevel = 30
                    },
                    new User()
                    {
                        Id = 3
                    }
                }.AsEnumerable()));
            groupApiClientMock
                .Setup(service => service.GetMembersByGroupIdAsync(11))
                .Returns(Task.FromResult(new List<User>()
                {
                    new User()
                    {
                        Id = 3
                    }
                }.AsEnumerable()));

            groupApiClientMock
                .Setup(service => service.AddUserToGroup(It.IsAny<int>(), It.IsAny<int>(), It.IsInRange(0, 50, Range.Inclusive)))
                .Returns<int, int, int, string>((gid, uid, alvl, exprat) => Task.FromResult(new User()
                {
                    Id = uid,
                    AccessLevel = alvl
                }));

            return groupApiClientMock.Object;
        }

        private IProjectApiClient PrepareMockProjectApiClient()
        {
            return new Mock<IProjectApiClient>().Object;
        }
        private IStandaloneApiClient PrepareMockStandaloneApiClient()
        {
            return new Mock<IStandaloneApiClient>().Object;
        }

        private GroupController PrepareGroupController(int userId)
        { 
            var amConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EngagementPointsProfile>();
                cfg.AddProfile<ReportedTimeProfile>();
            });

            var groupController = new GroupController(null,new Mapper(amConfig), new GitLabGroupRepository(_groupApiClient), new GitLabStandaloneRepository(_standaloneApiClient),  new GitLabProjectRepository(_projectApiClient), 
                new GitLabInfoDbRepository(Mock.Of<ILogger<GitLabInfoDbRepository>>(), _dbFixture.GetGitLabInfoDbContext()))
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(
                            new[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                                new Claim(ClaimTypes.Name, "Jan Testowy")
                            }, "GitLab"))
                    }
                }
            };

            return groupController;
        }
    }
    
}
