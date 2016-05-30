using BGL.Web.GitService;
using BGL.Web.ViewModels;
using BGL.Web.Rules.Validation;
using System;
using System.Linq;
using BGL.Web.Rules.Validation;
using Airborne.Logging;
using Airborne.Notifications;
using Airborne;

namespace BGL.Web.Actions
{
    public class GetUserRepositoriesAction<T> where T : class
    {
        private IGitService GitService;

        private ILogger Logger;

        public Func<UserRepositoriesViewModel, T> OnSuccess { get; set; }

        public Func<NotificationCollection, T> OnFailed { get; set; }

        public Func<ErrorViewModel, T> OnError { get; set; }

        public GetUserRepositoriesAction(GitService.IGitService gitService, ILogger logger)
        {
            Guard.ArgumentNotNull(gitService, "gitService");
            Guard.ArgumentNotNull(logger, "logger");

            this.GitService = gitService;
            this.Logger = logger;
        }

        public T Execute(string userName)
        {
            Guard.ArgumentNotNull(OnSuccess, "OnSuccess");
            Guard.ArgumentNotNull(OnFailed, "OnFailed");

            var model = new UserRepositoriesViewModel();
            var userResult = GitService.LoadGitUser(new BGL.Services.Api.Models.Request.GetUserRequest(userName));

            var notifications = new NotificationCollection();

            if (!userResult.IsValid())
            {
                notifications.AddError("No user was found with the username {0}".FormatInvariantCulture(userName));
            }

            if (!notifications.HasErrors())
            {
                var repoResult = GitService.GetRepositories(new BGL.Services.Api.Models.Request.GetRepositoriesRequest() { Username = userName });

                if (repoResult.IsNull() || repoResult.Repositories.IsNull())
                {
                    notifications.AddException(new Exception("An error occured while retrieving the users repository."));
                }

                if (!notifications.HasErrors())
                {
                    model.User = userResult.User;
                    model.Repositories = repoResult.Repositories;
                }
            }

            return notifications.Any() ?OnFailed(notifications): OnSuccess(model);
        }
    }
}