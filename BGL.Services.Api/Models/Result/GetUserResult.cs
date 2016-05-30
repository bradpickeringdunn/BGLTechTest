using Airborne.Services.ClientAdapter.Results;
using BGL.Services.Api.Models.Dto;
using System.Runtime.Serialization;

namespace BGL.Services.Api.Models.Result
{
    [DataContract]
    public class GetUserResult : GenericServiceResult
    {
        [DataMember]
        public UserDto User { get; set; }

        public GetUserResult()
        {
            this.User = new UserDto();
        }
        
    }
}
