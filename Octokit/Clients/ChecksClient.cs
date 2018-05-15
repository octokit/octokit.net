namespace Octokit
{
    public class ChecksClient : IChecksClient
    {
        public ICheckRunsClient Run { get; private set; }
        public ICheckSuitesClient Suite { get; private set; }

        public ChecksClient(ApiConnection apiConnection)
        {
            Run = new CheckRunsClient(apiConnection);
            Suite = new CheckSuitesClient(apiConnection);
        }
    }
}