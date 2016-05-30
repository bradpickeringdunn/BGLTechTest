using Airborne.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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