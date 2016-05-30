using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using BGL.Web.GitService;
using BGL.Web.Actions;
using BGL.Services.Api.Models.Result;
using BGL.Services.Api.Models.Dto;
using Airborne.Logging;
using System.Collections.Generic;

namespace BGL.Web.Tests.Actions
{
    [TestClass]
    public class GetUserRepositoriesActionTestFixture
    {
        [TestMethod]
        public void Ensure_Action_Returns_OnSuccess()
        {
            var logger = A.Fake<ILogger>();
            var gitService = A.Fake<IGitService>();

            var user = new UserDto()
            {
                AvatarUrl = "Url",
                Location = "location",
                Name = "Test user"
            };

            A.CallTo(() => gitService.LoadGitUser(null)).WithAnyArguments().Returns(new GetUserResult()
            {
                User = user
            });

            var repos = new List<GitRepositoryDto>()
                {
                    new GitRepositoryDto() {Name = "Test repo", StargazersCount = 12 },
                    new GitRepositoryDto() {Name = "Test repo 1", StargazersCount = 15 },
                    new GitRepositoryDto() {Name = "Test repo 2", StargazersCount = 125 },
                    new GitRepositoryDto() {Name = "Test repo 3", StargazersCount = 142 },
                    new GitRepositoryDto() {Name = "Test repo 4", StargazersCount = 132 },
                    new GitRepositoryDto() {Name = "Test repo 5", StargazersCount = 1 },
                    new GitRepositoryDto() {Name = "Test repo 6", StargazersCount = 122 }
                };

            A.CallTo(() => gitService.GetRepositories(null)).WithAnyArguments().Returns(new GetRepositoriesResult()
            {
                Repositories = repos
            });

            var result = new GetUserRepositoriesAction<dynamic>(gitService, logger)
            {
                OnSuccess = (m) => new { success = true, model = m },
                OnFailed = (m) => new { success = false },
                OnError = (errors) => new {inerror = false}
            }.Execute("username");

            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.model);
            Assert.AreEqual(result.model.User.Name, user.Name);
            Assert.AreEqual(result.model.User.AvatarUrl, user.AvatarUrl);
            Assert.AreEqual(result.model.User.Location, user.Location);

            Assert.IsTrue(result.model.Repositories.Count > 0);
        }

        [TestMethod]
        public void Ensure_Action_Returns_User_If_Repository_Is_Empty()
        {
            var logger = A.Fake<ILogger>();
            var gitService = A.Fake<IGitService>();

            var user = new UserDto()
            {
                AvatarUrl = "Url",
                Location = "location",
                Name = "Test user"
            };

            A.CallTo(() => gitService.LoadGitUser(null)).WithAnyArguments().Returns(new GetUserResult()
            {
                User = user
            });

            var repos = new List<GitRepositoryDto>()
                {};

            A.CallTo(() => gitService.GetRepositories(null)).WithAnyArguments().Returns(new GetRepositoriesResult()
            {
                Repositories = repos
            });

            var result = new GetUserRepositoriesAction<dynamic>(gitService, logger)
            {
                OnSuccess = (m) => new { success = true, model = m },
                OnFailed = (m) => new { success = false },
                OnError = (errors) => new { inerror = false }
            }.Execute("username");

            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.model);
            Assert.AreEqual(result.model.User.Name, user.Name);
            Assert.AreEqual(result.model.User.AvatarUrl, user.AvatarUrl);
            Assert.AreEqual(result.model.User.Location, user.Location);

            Assert.IsTrue(result.model.Repositories.Count == 0);
        }

        [TestMethod]
        public void Ensure_Action_Returns_OnError_If_User_Is_Null()
        {
            var logger = A.Fake<ILogger>();
            var gitService = A.Fake<IGitService>();

            A.CallTo(() => gitService.LoadGitUser(null)).WithAnyArguments().Returns(null);

            var repos = new List<GitRepositoryDto>()
            { };

            A.CallTo(() => gitService.GetRepositories(null)).WithAnyArguments().Returns(new GetRepositoriesResult()
            {
                Repositories = repos
            });

            var result = new GetUserRepositoriesAction<dynamic>(gitService, logger)
            {
                OnSuccess = (m) => new { success = true, model = m },
                OnFailed = (m) => new { success = false },
                OnError = (errors) => new { error = true, notifications = errors }
            }.Execute("username");

            Assert.IsTrue(result.error);
        }

        [TestMethod]
        public void Ensure_Action_Returns_OnError_If_UserRepository_Is_Null()
        {
            var logger = A.Fake<ILogger>();
            var gitService = A.Fake<IGitService>();

            var user = new UserDto()
            {
                AvatarUrl = "Url",
                Location = "location",
                Name = "Test user"
            };

            A.CallTo(() => gitService.LoadGitUser(null)).WithAnyArguments().Returns(new GetUserResult()
            {
                User = user
            });

            A.CallTo(() => gitService.GetRepositories(null)).WithAnyArguments().Returns(null);

            var result = new GetUserRepositoriesAction<dynamic>(gitService, logger)
            {
                OnSuccess = (m) => new { success = true, model = m },
                OnFailed = (m) => new { success = false },
                OnError = (errors) => new { error = true, notifications = errors }
            }.Execute("username");

            Assert.IsTrue(result.error);
        }

        [TestMethod]
        public void Ensure_Action_Returns_Returns_A_Sorted_Repository_By_StargazersCount_Desc()
        {
            var logger = A.Fake<ILogger>();
            var gitService = A.Fake<IGitService>();

            var user = new UserDto()
            {
                AvatarUrl = "Url",
                Location = "location",
                Name = "Test user"
            };

            A.CallTo(() => gitService.LoadGitUser(null)).WithAnyArguments().Returns(new GetUserResult()
            {
                User = user
            });

            var repos = new List<GitRepositoryDto>()
                {
                    new GitRepositoryDto() {Name = "Test repo", StargazersCount = 12 },
                    new GitRepositoryDto() {Name = "Test repo 1", StargazersCount = 15 },
                    new GitRepositoryDto() {Name = "Test repo 2", StargazersCount = 125 },
                    new GitRepositoryDto() {Name = "Test repo 3", StargazersCount = 142 },
                    new GitRepositoryDto() {Name = "Test repo 4", StargazersCount = 132 },
                    new GitRepositoryDto() {Name = "Test repo 5", StargazersCount = 1 },
                    new GitRepositoryDto() {Name = "Test repo 6", StargazersCount = 122 }
                };

            A.CallTo(() => gitService.GetRepositories(null)).WithAnyArguments().Returns(new GetRepositoriesResult()
            {
                Repositories = repos
            });

            var result = new GetUserRepositoriesAction<dynamic>(gitService, logger)
            {
                OnSuccess = (m) => new { success = true, model = m },
                OnFailed = (m) => new { failed = false },
                OnError = (errors) => new {inError = false}
            }.Execute("username");

            var sortedRepo = (from r in repos
                              orderby r.StargazersCount descending
                              select r)
                              .ToList();


            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.model);
            
            for(var i = 0; i < result.model.Repositories.Count; i++)
           { 
                Assert.AreEqual(sortedRepo[i].Name, result.model.Repositories[i].Name);
                Assert.AreEqual(sortedRepo[i].StargazersCount, result.model.Repositories[i].StargazersCount);
            }
        }
    }
}
