﻿using Octokit.Internal;

namespace Octokit.Reactive
{
    public interface IObservableGitHubClient
    {
        IConnection Connection { get; }

        IObservableAuthorizationsClient Authorization { get; }
        IObservableMiscellaneousClient Miscellaneous { get; }
        IObservableOrganizationsClient Organization { get; }
        IObservableRepositoriesClient Repository { get; }
        IObservableSshKeysClient SshKey { get; }
        IObservableUsersClient User { get; }
    }
}