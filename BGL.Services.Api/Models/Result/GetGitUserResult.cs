using Airborne.Services.ClientAdapter.Results;
using BGL.Services.Api.Models.Dto;
using System.Runtime.Serialization;

namespace BGL.Services.Api.Models.Result
{
    [DataContract]
    public class GetGitUserResult : GenericServiceResult
    {
        [DataMember]
        public GitUserDto User { get; set; }

        public GetGitUserResult()
        {
            this.User = new GitUserDto();
        }
        
    }
}
