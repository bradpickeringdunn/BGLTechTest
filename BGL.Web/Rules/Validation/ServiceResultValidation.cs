using BGL.Services.Api.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGL.Web.Rules.Validation
{
    public static class ServiceResultValidation
    {
        public static bool IsValid(this GetGitUserResult result)
        {
            var status = false;

            if (result.IsNotNull() && result.User.IsNotNull())
            {
                var user = result.User;

                if (user.Name.IsNotNullOrEmpty() || user.AvatarUrl.IsNotNullOrEmpty() || user.Location.IsNotNullOrEmpty())
                {
                    status = true;
                }
            }

            return status;
        }
        
    }
}