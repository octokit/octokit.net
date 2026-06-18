using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Milestones API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/milestones/">Issue Milestones API documentation</a> for more information.
    /// </remarks>
    public class ObservableMilestonesClient : IObservableMilestonesClient
    {
        readonly IMilestonesClient _client;
        readonly IConnection _connection;

        public ObservableMilestonesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Issue.Milestone;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets a single Milestone by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#get-a-single-milestone
        /// </remarks>
        /// <returns></returns>
        public IObservable<Milestone> Get(string owner, string name, int milestoneNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, milestoneNumber, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single Milestone by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#get-a-single-milestone
        /// </remarks>
        /// <returns></returns>
        public IObservable<Milestone> Get(long repositoryId, int milestoneNumber, CancellationToken cancellationToken = default)
        {
            return _client.Get(repositoryId, milestoneNumber, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<Milestone> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns></returns>
        public IObservable<Milestone> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public IObservable<Milestone> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Milestone>(ApiUrls.Milestones(owner, name), options, cancellationToken);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public IObservable<Milestone> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Milestone>(ApiUrls.Milestones(repositoryId), options);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of Milestones returned</param>
        /// <returns></returns>
        public IObservable<Milestone> GetAllForRepository(string owner, string name, MilestoneRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of Milestones returned</param>
        /// <returns></returns>
        public IObservable<Milestone> GetAllForRepository(long repositoryId, MilestoneRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of Milestones returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public IObservable<Milestone> GetAllForRepository(string owner, string name, MilestoneRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Milestone>(ApiUrls.Milestones(owner, name),
                request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of Milestones returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public IObservable<Milestone> GetAllForRepository(long repositoryId, MilestoneRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Milestone>(ApiUrls.Milestones(repositoryId), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Creates a milestone for the specified repository. Any user with pull access to a repository can create a
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#create-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newMilestone">A <see cref="NewMilestone"/> instance describing the new Milestone to create</param>
        /// <returns></returns>
        public IObservable<Milestone> Create(string owner, string name, NewMilestone newMilestone, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newMilestone, nameof(newMilestone));

            return _client.Create(owner, name, newMilestone, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a milestone for the specified repository. Any user with pull access to a repository can create a
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#create-a-milestone</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newMilestone">A <see cref="NewMilestone"/> instance describing the new Milestone to create</param>
        /// <returns></returns>
        public IObservable<Milestone> Create(long repositoryId, NewMilestone newMilestone, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newMilestone, nameof(newMilestone));

            return _client.Create(repositoryId, newMilestone, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates a milestone for the specified repository. Any user with pull access to a repository can create a
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#update-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="milestoneNumber">The Milestone number</param>
        /// <param name="milestoneUpdate">An <see cref="MilestoneUpdate"/> instance describing the changes to make to the Milestone
        /// </param>
        /// <returns></returns>
        public IObservable<Milestone> Update(string owner, string name, int milestoneNumber, MilestoneUpdate milestoneUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(milestoneUpdate, nameof(milestoneUpdate));

            return _client.Update(owner, name, milestoneNumber, milestoneUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates a milestone for the specified repository. Any user with pull access to a repository can create a
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#update-a-milestone</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="milestoneNumber">The Milestone number</param>
        /// <param name="milestoneUpdate">An <see cref="MilestoneUpdate"/> instance describing the changes to make to the Milestone
        /// </param>
        /// <returns></returns>
        public IObservable<Milestone> Update(long repositoryId, int milestoneNumber, MilestoneUpdate milestoneUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(milestoneUpdate, nameof(milestoneUpdate));

            return _client.Update(repositoryId, milestoneNumber, milestoneUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#delete-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="milestoneNumber">The Milestone number</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string name, int milestoneNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, milestoneNumber, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#delete-a-milestone</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="milestoneNumber">The Milestone number</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(long repositoryId, int milestoneNumber, CancellationToken cancellationToken = default)
        {
            return _client.Delete(repositoryId, milestoneNumber, cancellationToken).ToObservable();
        }
    }
}
