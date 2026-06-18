using System;
using System.Reactive.Threading.Tasks;
using System.Threading;

namespace Octokit.Reactive
{
    public class ObservableMergingClient : IObservableMergingClient
    {
        readonly IMergingClient _client;

        public ObservableMergingClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));
            _client = client.Repository.Merging;
        }

        /// <summary>
        /// Create a merge for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/merging/#perform-a-merge
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="merge">The merge to create</param>
        /// <returns></returns>
        public IObservable<Merge> Create(string owner, string name, NewMerge merge, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(merge, nameof(merge));

            return _client.Create(owner, name, merge, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Create a merge for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/merging/#perform-a-merge
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="merge">The merge to create</param>
        /// <returns></returns>
        public IObservable<Merge> Create(long repositoryId, NewMerge merge, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(merge, nameof(merge));

            return _client.Create(repositoryId, merge, cancellationToken).ToObservable();
        }
    }
}
