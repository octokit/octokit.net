using Octokit.Http;

namespace Octokit
{
    public interface IGitHubClient
    {
        IConnection Connection { get; }

        IAuthorizationsClient Authorization { get; }
        IAutoCompleteClient AutoComplete { get; }
        IOrganizationsClient Organization { get; }
        IRepositoriesClient Repository { get; }
        ISshKeysClient SshKey { get; }
        IUsersClient User { get; }
    }
}
