using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using RestSharp;
using BGL.Services.GitServices;
using BGL.Services.Api.Models.Request;
using Airborne.Logging;
using System.Text;
using System.Collections.Generic;
using BGL.Services.Api.Models.Result;
using BGL.Services.Api.Models.Dto;
using Newtonsoft.Json;

namespace BGL.Services.Tests.GitServices
{
    [TestClass]
    public class GitServiceTestFixture
    {
        [TestMethod]
        public void Unsure_A_Propperly_Formatted_User_Is_Returned()
        {
            var logger = A.Fake<ILogger>();
            var restClient = A.Fake<IRestClient>();

            var avatarUrl = "https://avatars.githubusercontent.com/u/78586?v=3";
            var name = "Rob Conery";
            var location = "Seattle, WA";

            A.CallTo(() => restClient.Execute(null)).WithAnyArguments().Returns(UserJson());

            var userResult = new GitService(logger, restClient).LoadGitUser(new GetGitUserRequest("Username"));

            Assert.IsNotNull(userResult);
            Assert.IsNotNull(userResult.User);
            Assert.AreEqual(avatarUrl, userResult.User.AvatarUrl);
            Assert.AreEqual(name, userResult.User.Name);
            Assert.AreEqual(location, userResult.User.Location);

            A.CallTo(() => logger.Error(string.Empty)).MustNotHaveHappened();
        }

        [TestMethod]
        public void Unsure_A_Propperly_Formatted_UserRepository_Is_Returned()
        {
            var logger = A.Fake<ILogger>();
            var restClient = A.Fake<IRestClient>();

            var jsonResult = RepositoryJson();
            A.CallTo(() => restClient.Execute(null)).WithAnyArguments().Returns(jsonResult);

            var userRepository = new GitService(logger, restClient).GetRepositories(new GetGitRepositoriesRequest() {Username = "test name" });

            Assert.IsNotNull(userRepository);
            Assert.IsNotNull(userRepository.Repositories.Any());

            foreach (var repo in userRepository.Repositories)
            {
            }
            A.CallTo(() => logger.Error(string.Empty)).MustNotHaveHappened();
        }

        private RestResponse UserJson()
        {
            var result = "{'avatar_url': 'https://avatars.githubusercontent.com/u/78586?v=3', ";
            result += "'name': 'Rob Conery',";
            result += "'location': 'Seattle, WA'}";
            
            return new RestResponse() { Content = result };
        }
        
        private RestResponse RepositoryJson()
        {
            var result = "{\n  \"login\": \"robconery\",\n  \"id\": 78586,\n  \"avatar_url\": \"https://avatars.githubusercontent.com/u/78586?v=3\",\n  \"gravatar_id\": \"\",\n  \"url\": \"https://api.github.com/users/robconery\",\n  \"html_url\": \"https://github.com/robconery\",\n  \"followers_url\": \"https://api.github.com/users/robconery/followers\",\n  \"following_url\": \"https://api.github.com/users/robconery/following{/other_user}\",\n  \"gists_url\": \"https://api.github.com/users/robconery/gists{/gist_id}\",\n  \"starred_url\": \"https://api.github.com/users/robconery/starred{/owner}{/repo}\",\n  \"subscriptions_url\": \"https://api.github.com/users/robconery/subscriptions\",\n  \"organizations_url\": \"https://api.github.com/users/robconery/orgs\",\n  \"repos_url\": \"https://api.github.com/users/robconery/repos\",\n  \"events_url\": \"https://api.github.com/users/robconery/events{/privacy}\",\n  \"received_events_url\": \"https://api.github.com/users/robconery/received_events\",\n  \"type\": \"User\",\n  \"site_admin\": false,\n  \"name\": \"Rob Conery\",\n  \"company\": \"BigMachine\",\n  \"blog\": \"http://wekeroad.com\",\n  \"location\": \"Seattle, WA\",\n  \"email\": \"rob@wekeroad.com\",\n  \"hireable\": null,\n  \"bio\": null,\n  \"public_repos\": 37,\n  \"public_gists\": 29,\n  \"followers\": 1229,\n  \"following\": 0,\n  \"created_at\": \"2009-04-28T02:08:55Z\",\n  \"updated_at\": \"2016-05-20T16:40:32Z\"\n}\n";
            return new RestResponse() { Content = result };
        }
    }
}
