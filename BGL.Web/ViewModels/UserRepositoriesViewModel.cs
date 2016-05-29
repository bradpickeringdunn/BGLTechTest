using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGL.Web.ViewModels
{
    public class UserRepositoriesViewModel
    {
        public UserRepositoriesViewModel()
        {
            this.Repositories = new List<GBL.Service.Api.Models.Dto.GitRepositoryDto>();
            this.User = new GBL.Service.Api.Models.Dto.UserDto();
        }

        public GBL.Service.Api.Models.Dto.UserDto User { get; set; }

        public IList<GBL.Service.Api.Models.Dto.GitRepositoryDto> Repositories { get; set; }
    }
}