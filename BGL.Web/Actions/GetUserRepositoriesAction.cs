using Backbone.Logging;
using Backbone.Utilities;
using BGL.Web.GitService;
using BGL.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGL.Web.Actions
{
    public class GetUserRepositoriesAction<T> where T : class
    {
        private IGitService GitService;

        private ILogger Logger;

        public Func<UserRepositoriesViewModel, T> OnSuccess { get; set; }

        public Func<BaseViewModel, T> OnFailed { get; set; }

        public GetUserRepositoriesAction(GitService.IGitService gitService, ILogger logger)
        {
            Guardian.ArgumentNotNull(gitService, "gitService");
            Guardian.ArgumentNotNull(logger, "logger");

            this.GitService = gitService;
            this.Logger = logger;
        }

        public T Execute(string userName)
        {
            var service = new GitService.GitServiceClient();
            var x = service.LoadGitUser(new GBL.Service.Api.Models.Request.GetUserRequest(userName));

            var y = service.GetRepositories(new GBL.Service.Api.Models.Request.GetRepositoriesRequest() { Username = userName });

            var model = new UserRepositoriesViewModel()
            {
                User = x.User,
                Repositories = y.Repositories
            };

            return OnSuccess(model);
        }
    }
}