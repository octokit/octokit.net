using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Dependency Review API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/dependency-graph/dependency-review">Git Dependency Review API documentation</a> for more information.
    /// </remarks>
    public class ObservableDependencyReviewClient : IObservableDependencyReviewClient
    {
        readonly IDependencyReviewClient _client;

        public ObservableDependencyReviewClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.DependencyGraph.DependencyReview;
        }

        /// <summary>
        /// Gets all <see cref="DependencyDiff"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/dependency-graph/dependency-review">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The base revision</param>
        /// <param name="head">The head revision</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<DependencyDiff> GetAll(string owner, string name, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return _client.GetAll(owner, name, @base, head).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Gets all <see cref="DependencyDiff"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/dependency-graph/dependency-review">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The base revision</param>
        /// <param name="head">The head revision</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<DependencyDiff> GetAll(long repositoryId, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return _client.GetAll(repositoryId, @base, head).ToObservable().SelectMany(x => x);
        }
    }
}
