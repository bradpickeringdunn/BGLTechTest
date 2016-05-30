using System.Runtime.Serialization;

namespace BGL.Services.Api.Models.Request
{
    [DataContract]
    public class GetGitRepositoriesRequest
    {
        [DataMember]
        public string Username { get; set; }
    }
}
