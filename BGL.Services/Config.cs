using System.Configuration;
using System;
using Airborne;

namespace BGL.Services
{
    public static class Config
    {
        public static string GitUserUrl
        {
            get
            {
                var url = ConfigurationManager.AppSettings["GitUserUrl"];
                Guard.ArgumentNotNull(url, "url");
                return url;
            }
        }
    }
}
