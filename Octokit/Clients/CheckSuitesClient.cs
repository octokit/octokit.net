namespace Octokit
{
    public class CheckSuitesClient : ApiClient, ICheckSuitesClient
    {
        public CheckSuitesClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}