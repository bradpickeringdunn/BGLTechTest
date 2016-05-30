using System.Runtime.Serialization;

namespace BGL.Services.Api.Models.Request
{
    [DataContract]
    public class GetGitUserRequest
    {
        [DataMember]
        public string Username { get; set; }

        public GetGitUserRequest(string username)
        {
            this.Username = username;
        }
    }
}
