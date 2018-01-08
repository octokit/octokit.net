using System.Threading.Tasks;

namespace Octokit
{
    public class ApplicationClient : ApiClient, IApplicationClient
    {
        public ApplicationClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<Application> Create()
        {
            return ApiConnection.Get<Application>(ApiUrls.App(), null, AcceptHeaders.MachineManPreview);
        }
    }
}