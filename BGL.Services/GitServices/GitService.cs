using BGL.Services.Api.Contracts;
using RestSharp;
using BGL.Services.Api.Models.Result;
using BGL.Services.Api.Models.Request;
using BGL.Services.Api.Models.Dto;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using BGL.Services.GitServices.Models;
using Airborne;
using Airborne.Logging;
using Airborne.Notifications;

namespace BGL.Services.GitServices
{
    /// <summary>
    /// Concrete Git service implementation
    /// </summary>
    public class GitService : BaseService, IGitService
    {
        private IRestClient RestClient;

        public GitService(ILogger logger, IRestClient restClient)
            : base(logger)
        {
            Guard.ArgumentNotNull(restClient, "restClient");
            this.RestClient = restClient;
        }

        /// <summary>
        /// Gets a users Git repositories 
        /// </summary>
        public GetGitRepositoriesResult GetGitUserRepositories(GetGitRepositoriesRequest request)
        {
            return TryExecute<GetGitRepositoriesResult>(request, (result) =>
            {
                Guard.ArgumentNotNull(request, "request");
                Guard.IsTrue(request.Username.IsNotNullOrEmpty());

                var query = string.Format("{0}/{1}", request.Username, "repos");
                
                RestClient.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
                var restRequest = new RestRequest(query,Method.GET);

                var queryResult = RestClient.Execute(restRequest);

                if (query.IsNullOrEmpty())
                {
                    result.Notifications.AddMessage(new Notification("No results were returned."));
                }

                if (!result.Notifications.Any())
                {
                    var repos = JsonConvert.DeserializeObject<List<GitRepositoryModel>>(queryResult.Content);
                    Guard.ArgumentNotNull(repos, "repos");

                    foreach (var repo in repos)
                    {
                        result.Repositories.Add(new GitRepositoryDto()
                        {
                            Name = repo.name,
                            StargazersCount = repo.stargazers_count
                        });
                    }
                }
            });
        }
                
        /// <summary>
        /// Gets a Git User
        /// </summary>
                    
        public GetGitUserResult GetGitUser(GetGitUserRequest request)
        {
            return TryExecute<GetGitUserResult>(request, (result) =>
          {
              Guard.ArgumentNotNull(request, "request");
                            
              RestClient.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
              var restRequest = new RestRequest(request.Username,Method.GET);

              var queryResult = RestClient.Execute(restRequest);

              var user = JsonConvert.DeserializeObject<GitUserModel>(queryResult.Content);

              result.User.Name = user.name;
              result.User.AvatarUrl = user.avatar_url;
              result.User.Location = user.location;

          });
        }
    }
}


