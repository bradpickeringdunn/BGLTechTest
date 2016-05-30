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
            this.Repositories = new List<BGL.Services.Api.Models.Dto.GitRepositoryDto>();
            this.User = new BGL.Services.Api.Models.Dto.UserDto();
        }

        public BGL.Services.Api.Models.Dto.UserDto User { get; set; }

        public IList<BGL.Services.Api.Models.Dto.GitRepositoryDto> Repositories { get; set; }
    }
}