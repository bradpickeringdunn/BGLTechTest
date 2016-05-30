
using BGL.Web.ViewModels;
using BGL.Web.Rules.Validation;
using System;
using System.Linq;
using Airborne.Logging;
using Airborne.Notifications;
using Airborne;
using BGL.Services.Api.Models.Request;
using BGL.Services.Api.Models.Dto;
using System.Collections.Generic;
using BGL.Web.Localization;

namespace BGL.Web.Actions
{
    public class GetUserRepositoriesAction<T> where T : class
    {
        private GitService.IGitService GitService;

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

            var repoResult = GitService.GetGitUserRepositories(new GetGitRepositoriesRequest() { Username = username });

            if (repoResult.IsNull() || repoResult.Repositories.IsNull())
            {
                Logger.Error("The repository result returned was null. Username - {0}".FormatInvariantCulture(username));
                Notifications.AddError(Errors.GetGitRepositoryError.FormatInvariantCulture(username));
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

        private GitUserDto GetGitUser(string username)
        {
            var user = new GitUserDto();
            var userResult = GitService.GetGitUser(new GetGitUserRequest(username));

            if (userResult.IsNull() || userResult.User.IsNull())
            {
                Logger.Error("The user returned by the git service was null. Username - {0}".FormatInvariantCulture(username));
                Notifications.AddError(Errors.GetGitUserError.FormatInvariantCulture(username));
            }

            if (!Notifications.HasErrors())
            {
                user = userResult.User;
            }

            if (!Notifications.HasErrors() &&!userResult.IsValid())
            {
                Notifications.AddMessage(new Notification(Messages.NoUserFound.FormatInvariantCulture(username)));
            }

            return user;
        }
    }
}