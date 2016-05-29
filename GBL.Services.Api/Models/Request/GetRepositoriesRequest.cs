using System.Runtime.Serialization;

namespace GBL.Service.Api.Models.Request
{
    [DataContract]
    public class GetRepositoriesRequest
    {
        [DataMember]
        public string Username { get; set; }
    }
}
