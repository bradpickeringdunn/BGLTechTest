using GBL.Services.Api.Contracts;
using System.Threading.Tasks;
using RestSharp;
using GBL.Service.Api.Models.Result;
using GBL.Service.Api.Models.Request;
using GBL.Service.Api.Models.Dto;
using GBL.Service;
using System;
using Backbone.Logging;

namespace GBL.Services
{
    public class GitService : BaseService, IGitService
    {
        public GitService(ILogger logger)
            :base(logger)
        {}

        public GetUserResult LoadGitUser(GetUserRequest request)
        {
            return TryExecute<GetUserResult>(request, (result) =>
          {
              var client = new RestClient(Config.GitUserUrl);

              client.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
              var restRequest = new RestRequest(Method.GET);

              var queryResult = client.Execute(restRequest);

          });
        }
    }
}


