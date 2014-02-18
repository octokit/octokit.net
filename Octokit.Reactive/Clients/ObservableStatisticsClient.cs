using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableStatisticsClient : IObservableStatisticsClient
    {
        readonly IGitHubClient _client;

        public ObservableStatisticsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            _client = client;
        }

        public IObservable<IEnumerable<Contributor>> GetContributors(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Statistics.GetContributors(owner, repositoryName).ToObservable();
        }
    }
}