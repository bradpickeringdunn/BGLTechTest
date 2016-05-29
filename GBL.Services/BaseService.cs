using Backbone.Logging;
using Backbone.Services.Results;
using Backbone.Utilities;
using System;

namespace GBL.Service
{
    public class BaseService
    {
        private ILogger logger { get; set; }

        public BaseService(ILogger logger)
        {
            Guardian.ArgumentNotNull(logger, "logger");
            this.logger = logger;
        }

        public T TryExecute<T>(object request, Action<T> action) where T: GenericServiceResult, new()
        {
            var result = new T();

            Guardian.ArgumentNotNull(request, "request");

            try
            {
                action(result);
            }
            catch(Exception ex)
            {
                logger.Exception(ex);
                result.Notifications.Add("A general service error occured.");
            }

            return result;
        }
    }
}
