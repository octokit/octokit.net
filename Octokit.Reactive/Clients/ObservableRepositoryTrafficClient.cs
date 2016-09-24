using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableRepositoryTrafficClient : IObservableRepositoryTrafficClient
    {
        readonly IRepositoryTrafficClient _client;

        public ObservableRepositoryTrafficClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Traffic;
        }

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        public IObservable<RepositoryTrafficPath> GetPaths(long repositoryId)
        {
            return _client.GetPaths(repositoryId).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<RepositoryTrafficPath> GetPaths(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.GetPaths(owner, name).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        public IObservable<RepositoryTrafficReferrer> GetReferrers(long repositoryId)
        {
            return _client.GetReferrers(repositoryId).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<RepositoryTrafficReferrer> GetReferrers(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.GetReferrers(owner, name).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get the total number of clones and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#clones</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        public IObservable<RepositoryTrafficCloneSummary> GetClones(long repositoryId, RepositoryTrafficRequest per)
        {
            Ensure.ArgumentNotNull(per, "per");

            return _client.GetClones(repositoryId, per).ToObservable();
        }

        /// <summary>
        /// Get the total number of clones and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#clones</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        public IObservable<RepositoryTrafficCloneSummary> GetClones(string owner, string name, RepositoryTrafficRequest per)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(per, "per");

            return _client.GetClones(owner, name, per).ToObservable();
        }

        /// <summary>
        /// Get the total number of views and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#views</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        public IObservable<RepositoryTrafficViewSummary> GetViews(long repositoryId, RepositoryTrafficRequest per)
        {
            Ensure.ArgumentNotNull(per, "per");

            return _client.GetViews(repositoryId, per).ToObservable();
        }

        /// <summary>
        /// Get the total number of views and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#views</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        public IObservable<RepositoryTrafficViewSummary> GetViews(string owner, string name, RepositoryTrafficRequest per)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(per, "per");

            return _client.GetViews(owner, name, per).ToObservable();
        }
    }
}
