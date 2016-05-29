using GBL.Services.Api.Contracts;
using System.Threading.Tasks;
using RestSharp;
using GBL.Service.Api.Models.Result;
using GBL.Service.Api.Models.Request;
using GBL.Service.Api.Models.Dto;
using GBL.Service;
using System;
using System.Linq;
using Backbone.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GBL.Services
{
    public class Juser
    {
        public string avatar_url { get; set; }

        public string name { get; set; }

        public string location { get; set; }
    }

    public class GitRepo
    {
        public int stargazers_count { get; set; }

        public string name { get; set; }
    }

    public class GitService : BaseService, IGitService
    {
        public GitService(ILogger logger)
            :base(logger)
        {}

        public GetRepositoriesResult GetRepositories(GetRepositoriesRequest request)
        {
            return TryExecute<GetRepositoriesResult>(request, (result) =>
            {
            var query = string.Format("{0}{1}/{2}", Config.GitUserUrl, request.Username, "repos");
            var client = new RestClient(query);

            client.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            var restRequest = new RestRequest(Method.GET);

            var queryResult = client.Execute(restRequest);

            var repos = JsonConvert.DeserializeObject<List<GitRepo>>(queryResult.Content);

            var a = (from r in repos
                     orderby r.stargazers_count descending
                     select r)
                          .Take(5).ToList();

                foreach (var repo in a)
                {
                    result.Repositories.Add(new GitRepositoryDto()
                    {
                        Name = repo.name,
                        StargazersCount = repo.stargazers_count
                    });
                }
                
            });
        }

        public GetUserResult LoadGitUser(GetUserRequest request)
        {
            return TryExecute<GetUserResult>(request, (result) =>
          {
              var query = string.Format("{0}{1}", Config.GitUserUrl, request.Username);
              var client = new RestClient(query);

              client.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
              var restRequest = new RestRequest(Method.GET);

              var queryResult = client.Execute(restRequest);

              var user = JsonConvert.DeserializeObject<Juser>(queryResult.Content);

              result.User.Name = user.name;
              result.User.AvatarUrl = user.avatar_url;
              result.User.Location = user.location;

          });
        }
    }
}


