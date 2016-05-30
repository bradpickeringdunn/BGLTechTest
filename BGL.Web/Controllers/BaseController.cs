using Airborne;
using Airborne.Logging;
using System.Web.Mvc;

namespace BGL.Web.Controllers
{

    public class BaseController : Controller
    {
        public ILogger Logger;

        public BaseController(ILogger logger)
        {
            Guard.ArgumentNotNull(logger, "logger");
            this.Logger = logger;
        }
    }
}