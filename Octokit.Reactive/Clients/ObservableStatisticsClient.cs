using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using Octokit.Response;

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

            return _client.Repository.Statistics.GetContributors(owner, repositoryName).ToObservable();
        }

        public IObservable<CommitActivity> GetCommitActivity(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetCommitActivity(owner, repositoryName).ToObservable();
        }

        public IObservable<CodeFrequency> GetCodeFrequency(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetCodeFrequency(owner, repositoryName).ToObservable();
        }

        public IObservable<Participation> GetParticipation(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetParticipation(owner, repositoryName).ToObservable();
        }

        public IObservable<PunchCard> GetPunchCard(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetPunchCard(owner, repositoryName).ToObservable();
        }
    }
}