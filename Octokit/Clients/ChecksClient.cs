namespace Octokit
{
    public class ChecksClient : IChecksClient
    {
        public ICheckSuitesClient Suite { get; private set; }

        public ChecksClient(ApiConnection apiConnection)
        {
            Suite = new CheckSuitesClient(apiConnection);
        }
    }
}