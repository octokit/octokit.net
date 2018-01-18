using System.Threading.Tasks;

namespace Octokit
{
    public class ApplicationsClient : ApiClient, IApplicationsClient
    {
        public ApplicationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<Application> Create()
        {
            return ApiConnection.Get<Application>(ApiUrls.App(), null, AcceptHeaders.MachineManPreview);
        }
    }
}