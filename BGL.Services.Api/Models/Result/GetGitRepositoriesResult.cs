using Airborne.Services.ClientAdapter.Results;
using BGL.Services.Api.Models.Dto;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BGL.Services.Api.Models.Result
{
    [DataContract]
    public class GetGitRepositoriesResult: GenericServiceResult
    {
        [DataMember]
        public IList<GitRepositoryDto> Repositories { get; set; }

        public GetGitRepositoriesResult()
        {
            this.Repositories = new List<GitRepositoryDto>();
        }

    }
}
