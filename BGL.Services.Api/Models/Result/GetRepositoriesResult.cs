using Airborne.Services.ClientAdapter.Results;
using BGL.Services.Api.Models.Dto;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BGL.Services.Api.Models.Result
{
    [DataContract]
    public class GetRepositoriesResult: GenericServiceResult
    {
        [DataMember]
        public IList<GitRepositoryDto> Repositories { get; set; }

        public GetRepositoriesResult()
        {
            this.Repositories = new List<GitRepositoryDto>();
        }

    }
}
