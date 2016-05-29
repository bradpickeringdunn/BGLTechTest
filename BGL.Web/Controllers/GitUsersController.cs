using Backbone.Logging;
using System.Web.Mvc;

namespace BGL.Web.Controllers
{
    public class GitUsersController : BaseController
    {
        private ILogger logger = new DebugLogger();

        private GitService.IGitService gitService = new GitService.GitServiceClient();

            // GET: GitUsers
            [HttpGet]
        public ActionResult Index(string userName)
        {
            return new Actions.GetUserRepositoriesAction<ActionResult>(this.gitService, this.logger)
            {
                OnSuccess = (model) => View(Views.Views.GitUsers.UserRepositories, model)
            }.Execute(userName);
        }
    }
}