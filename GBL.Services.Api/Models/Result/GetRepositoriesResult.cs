using Backbone.Services.Results;
using GBL.Service.Api.Models.Dto;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GBL.Service.Api.Models.Result
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
