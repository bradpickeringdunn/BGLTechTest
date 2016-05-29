using System.Web.Mvc;

namespace BGL.Web.Controllers
{
    public class GitUsersController : BaseController
    {
        // GET: GitUsers
        [HttpGet]
        public ActionResult Index(string userName)
        {
            var service = new GitService.GitServiceClient();
            var x = service.LoadGitUser();

            return View();
        }
    }
}