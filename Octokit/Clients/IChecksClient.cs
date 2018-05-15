namespace Octokit
{
    public interface IChecksClient
    {
        ICheckRunsClient Run { get; }

        ICheckSuitesClient Suite { get; }
    }
}
