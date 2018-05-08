namespace Octokit
{
    public class ChecksClient : IChecksClient
    {
        public ICheckRunsClient Runs { get; private set; }
        public ICheckSuitesClient Suites { get; private set; }

        public ChecksClient(ApiConnection apiConnection)
        {
            Runs = new CheckRunsClient(apiConnection);
            Suites = new CheckSuitesClient(apiConnection);
        }
    }
}