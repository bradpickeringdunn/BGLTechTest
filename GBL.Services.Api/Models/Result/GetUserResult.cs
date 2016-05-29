using Backbone.Services.Results;
using GBL.Service.Api.Models.Dto;
using System.Runtime.Serialization;

namespace GBL.Service.Api.Models.Result
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
