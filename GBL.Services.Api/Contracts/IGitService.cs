using GBL.Service.Api.Models.Request;
using GBL.Service.Api.Models.Result;
using System.ServiceModel;

namespace GBL.Services.Api.Contracts
{
    [ServiceContract]
    public interface IGitService
    {
        [OperationContract]
        GetUserResult LoadGitUser(GetUserRequest request);

        [OperationContract]
        GetRepositoriesResult GetRepositories(GetRepositoriesRequest request);
    }
}
