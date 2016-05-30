using Airborne;
using Airborne.Logging;
using Airborne.Notifications;
using Airborne.Services.ClientAdapter.Results;
using System;

namespace BGL.Services
{
    public class BaseService
    {
        private ILogger logger { get; set; }

        public BaseService(ILogger logger)
        {
            Guard.ArgumentNotNull(logger, "logger");
            this.logger = logger;
        }

        public T TryExecute<T>(object request, Action<T> action) where T: GenericServiceResult, new()
        {
            var result = new T();

            Guard.ArgumentNotNull(request, "request");

            try
            {
                action(result);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                result.Notifications.AddMessage(new Notification("A general service error occured."));
            }

            return result;
        }
    }
}
