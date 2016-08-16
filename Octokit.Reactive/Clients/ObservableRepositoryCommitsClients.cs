using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Commits API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/commits/">Repository Commits API documentation</a> for more information.
    /// </remarks>
    public class ObservableRepositoryCommitsClient : IObservableRepositoryCommitsClient
    {
        readonly IConnection _connection;
        readonly IRepositoryCommitsClient _commit;

        public ObservableRepositoryCommitsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _connection = client.Connection;
            _commit = client.Repository.Commit;
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        public IObservable<CompareResult> Compare(string owner, string name, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");

            return _commit.Compare(owner, name, @base, head).ToObservable();
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        public IObservable<CompareResult> Compare(int repositoryId, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");

            return _commit.Compare(repositoryId, @base, head).ToObservable();
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference for the commit</param>
        public IObservable<GitHubCommit> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _commit.Get(owner, name, reference).ToObservable();
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference for the commit</param>
        public IObservable<GitHubCommit> Get(int repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _commit.Get(repositoryId, reference).ToObservable();
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<GitHubCommit> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, new CommitRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<GitHubCommit> GetAll(int repositoryId)
        {
            return GetAll(repositoryId, new CommitRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GitHubCommit> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return GetAll(owner, name, new CommitRequest(), options);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GitHubCommit> GetAll(int repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return GetAll(repositoryId, new CommitRequest(), options);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        public IObservable<GitHubCommit> GetAll(string owner, string name, CommitRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return GetAll(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        public IObservable<GitHubCommit> GetAll(int repositoryId, CommitRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return GetAll(repositoryId, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GitHubCommit> GetAll(string owner, string name, CommitRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<GitHubCommit>(ApiUrls.RepositoryCommits(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GitHubCommit> GetAll(int repositoryId, CommitRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<GitHubCommit>(ApiUrls.RepositoryCommits(repositoryId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The repository reference</param>
        public IObservable<string> GetSha1(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _commit.GetSha1(owner, name, reference).ToObservable();
        }

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The repository reference</param>
        public IObservable<string> GetSha1(int repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _commit.GetSha1(repositoryId, reference).ToObservable();
        }
    }
}
