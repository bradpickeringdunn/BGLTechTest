using Backbone.Utilities;
using System.Configuration;
using System;

namespace GBL.Service
{
    public static class Config
    {
        public static string GitUserUrl
        {
            get
            {
                var url = ConfigurationManager.AppSettings["GitUserUrl"];

                return url;

            }
        }
    }
}
