using Airborne.Notifications;

namespace BGL.Web.ViewModels
{
    public class BaseViewModel
    {
        public NotificationCollection Notifications { get; set; }

        public BaseViewModel()
        {
            this.Notifications = new NotificationCollection();
        }
    }
}