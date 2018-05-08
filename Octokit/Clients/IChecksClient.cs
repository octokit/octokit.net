namespace Octokit
{
    public interface IChecksClient
    {
        ICheckRunsClient Runs { get; }

        ICheckSuitesClient Suites { get; }
    }
}
