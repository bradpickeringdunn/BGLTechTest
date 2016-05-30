using System.Runtime.Serialization;

namespace BGL.Services.Api.Models.Request
{
    [DataContract]
    public class GetRepositoriesRequest
    {
        [DataMember]
        public string Username { get; set; }
    }
}
