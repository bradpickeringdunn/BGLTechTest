using System;
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
                OnFailed = (m) => new { success = false }
            }.Execute("username");

            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.model);
            Assert.AreEqual(result.model.User.Name, user.Name);
            Assert.AreEqual(result.model.User.AvatarUrl, user.AvatarUrl);
            Assert.AreEqual(result.model.User.Location, user.Location);

            Assert.AreEqual(result.model.Repositories.Count, repos.Count);
        }
    }
}
