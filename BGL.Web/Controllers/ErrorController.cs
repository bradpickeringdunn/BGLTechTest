using Airborne.Logging;
using System.Web.Mvc;

namespace BGL.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(ILogger logger)
        : base(logger){ }

        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
    }
}