using System.Collections.Generic;

namespace BGL.Web.ViewModels
{
    public class UserRepositoriesViewModel
    {
        public UserRepositoriesViewModel()
        {
            this.Repositories = new List<BGL.Services.Api.Models.Dto.GitRepositoryDto>();
            this.User = new BGL.Services.Api.Models.Dto.GitUserDto();
        }

        public BGL.Services.Api.Models.Dto.GitUserDto User { get; set; }

        public IList<BGL.Services.Api.Models.Dto.GitRepositoryDto> Repositories { get; set; }
    }
}