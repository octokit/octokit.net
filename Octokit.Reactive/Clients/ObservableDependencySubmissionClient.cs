using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Dependency Submission API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">Dependency Submission API documentation</a> for more details.
    /// </remarks>
    public class ObservableDependencySubmissionClient : IObservableDependencySubmissionClient
    {
        readonly IDependencySubmissionClient _client;

        public ObservableDependencySubmissionClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.DependencyGraph.DependencySubmission;
        }

        /// <summary>
        /// Creates a new dependency snapshot.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="snapshot">The dependency snapshot to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs</exception>
        /// <returns>A <see cref="DependencySnapshotSubmission"/> instance for the created snapshot</returns>
        public IObservable<DependencySnapshotSubmission> Create(string owner, string name, NewDependencySnapshot snapshot)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(snapshot, nameof(snapshot));

            return _client.Create(owner, name, snapshot).ToObservable();
        }

        /// <summary>
        /// Creates a new dependency snapshot.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="snapshot">The dependency snapshot to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs</exception>
        /// <returns>A <see cref="DependencySnapshotSubmission"/> instance for the created snapshot</returns>
        public IObservable<DependencySnapshotSubmission> Create(long repositoryId, NewDependencySnapshot snapshot)
        {
            Ensure.ArgumentNotNull(snapshot, nameof(snapshot));

            return _client.Create(repositoryId, snapshot).ToObservable();
        }
    }
}
