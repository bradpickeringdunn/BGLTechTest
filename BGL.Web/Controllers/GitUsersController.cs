using Airborne;
using Airborne.Logging;
using BGL.Web.ViewModels;
using BGL.Web.Views;
using System.Web.Mvc;

namespace BGL.Web.Controllers
{
    public class GitUsersController : BaseController
    {
        private GitService.IGitService GitService = new GitService.GitServiceClient();

        public GitUsersController(ILogger logger, GitService.IGitService gitService)
            :base(logger)
        {
            Guard.ArgumentNotNull(gitService, "gitService");

            this.GitService = gitService;
        }

            // GET: GitUsers
            [HttpGet]
        public ActionResult Index(UserRepositorySearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return new Actions.GetUserRepositoriesAction<ActionResult>(GitService, Logger)
                {
                    OnSuccess = (m) => View(ViewPath.UserRepositories, m),
                    OnFailed = (messages) =>
                    {
                        return View(ViewPath.SearchUserRepositories, new UserRepositorySearchViewModel()
                        {
                            Notifications = messages
                        });
                    },
                    OnError = (errors) => View(ViewPath.ErrorPage, errors)
                }.Execute(model.Username);
            }

            return View(Views.ViewPath.SearchUserRepositories,model);
        }
    }
}