using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableRepositoryCommitsClient : IObservableRepositoryCommitsClient
    {
        readonly IGitHubClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryCommitsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
            _connection = client.Connection;
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <returns></returns>
        public IObservable<CompareResult> Compare(string owner, string name, string @base, string head)
        {
            return _client.Repository.Commits.Compare(owner, name, @base, head).ToObservable();
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<GitHubCommit> GetAll(string owner, string name)
        {
            return GetAll(owner, name, new CommitRequest());
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <returns></returns>
        public IObservable<GitHubCommit> GetAll(string owner, string name, CommitRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<GitHubCommit>(ApiUrls.RepositoryCommits(owner, name),
                request.ToParametersDictionary());
        }
    }
}
