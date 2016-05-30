using System.Runtime.Serialization;

namespace BGL.Services.Api.Models.Dto
{
    [DataContract]
    public class GitRepositoryDto
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int StargazersCount { get; set; }
    }
}
