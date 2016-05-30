using BGL.Web.GitService;
using BGL.Web.ViewModels;
using BGL.Web.Rules.Validation;
using System;
using System.Linq;
using BGL.Web.Rules.Validation;
using Airborne.Logging;
using Airborne.Notifications;
using Airborne;
using BGL.Services.Api.Models.Request;
using BGL.Services.Api.Models.Dto;
using System.Collections.Generic;

namespace BGL.Web.Actions
{
    public class GetUserRepositoriesAction<T> where T : class
    {
        private IGitService GitService;

        private ILogger Logger;

        public Func<UserRepositoriesViewModel, T> OnSuccess { get; set; }

        public Func<NotificationCollection, T> OnFailed { get; set; }

        public Func<NotificationCollection, T> OnError { get; set; }

        private NotificationCollection Notifications { get; set; }

        public GetUserRepositoriesAction(GitService.IGitService gitService, ILogger logger)
        {
            Guard.ArgumentNotNull(gitService, "gitService");
            Guard.ArgumentNotNull(logger, "logger");

            this.GitService = gitService;
            this.Logger = logger;
            this.Notifications = NotificationCollection.CreateEmpty();
        }

        public T Execute(string username)
        {
            Guard.ArgumentNotNull(OnSuccess, "OnSuccess");
            Guard.ArgumentNotNull(OnFailed, "OnFailed");
            Guard.ArgumentNotNull(OnError, "OnError");
            Guard.ArgumentNotEmpty(username, "username");

            var model = new UserRepositoriesViewModel();

            model.User = GetGitUser(username);
            
            if (!Notifications.HasErrors())
            {
                model.Repositories = GetUserRepository(username);
            }

            if (Notifications.HasErrors())
            {
                return OnError(Notifications);
            }

            return Notifications.HasMessages() ?OnFailed(Notifications): OnSuccess(model);
        }

        private IList<GitRepositoryDto> GetUserRepository(string username)
        {
            var repository = new List<GitRepositoryDto>();

            var repoResult = GitService.GetRepositories(new GetRepositoriesRequest() { Username = username });

            if (repoResult.IsNull() || repoResult.Repositories.IsNull())
            {
                Notifications.AddError("An error occurred while retrieving the users repository.");
            }

            if (!Notifications.HasErrors())
            {
                repository = OrderByStargazers(repoResult.Repositories);
            }

            return repository;
        }

        private List<GitRepositoryDto> OrderByStargazers(IList<GitRepositoryDto> repositories)
        {
            return (from r in repositories
                    orderby r.StargazersCount descending
                    select r)
                    .Take(Config.RepositoryCount).ToList();
        }

        private UserDto GetGitUser(string username)
        {
            var user = new UserDto();
            var userResult = GitService.LoadGitUser(new GetUserRequest(username));

            if (userResult.IsNull() || userResult.User.IsNull())
            {
                Notifications.AddError(("An error occurred while retrieving the user."));
            }

            if (!Notifications.HasErrors())
            {
                user = userResult.User;
            }

            if (!Notifications.HasErrors() &&!userResult.IsValid())
            {
                Notifications.AddMessage(new Notification("No user was found with the user name {0}".FormatInvariantCulture(username)));
            }

            return user;
        }
    }
}