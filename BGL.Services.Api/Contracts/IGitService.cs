using BGL.Services.Api.Models.Request;
using BGL.Services.Api.Models.Result;
using System.ServiceModel;

namespace BGL.Services.Api.Contracts
{
    [ServiceContract]
    public interface IGitService
    {
        [OperationContract]
        GetGitUserResult LoadGitUser(GetGitUserRequest request);

        [OperationContract]
        GetGitRepositoriesResult GetRepositories(GetGitRepositoriesRequest request);
    }
}
