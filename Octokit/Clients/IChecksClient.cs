namespace Octokit
{
    public interface IChecksClient
    {
        ICheckSuitesClient Suite { get; }
    }
}
