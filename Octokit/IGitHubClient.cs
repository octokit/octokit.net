using Octokit.Internal;

namespace Octokit
{
    public interface IGitHubClient
    {
        IConnection Connection { get; }

        IAuthorizationsClient Authorization { get; }
        IMiscellaneousClient Miscellaneous { get; }
        IOrganizationsClient Organization { get; }
        IRepositoriesClient Repository { get; }
        IReleasesClient Release { get; }
        ISshKeysClient SshKey { get; }
        IUsersClient User { get; }
    }
}
