﻿using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/events/">Issue Events API documentation</a> for more information.
    /// </remarks>
    public class ObservableIssuesEventsClient : IObservableIssuesEventsClient
    {
        readonly IIssuesEventsClient _client;
        readonly IConnection _connection;

        public ObservableIssuesEventsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Issue.Events;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        public IObservable<EventInfo> GetAllForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForIssue(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        public IObservable<EventInfo> GetAllForIssue(long repositoryId, int number)
        {
            return GetAllForIssue(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<EventInfo> GetAllForIssue(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<EventInfo>(ApiUrls.IssuesEvents(owner, name, number), options);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<EventInfo> GetAllForIssue(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<EventInfo>(ApiUrls.IssuesEvents(repositoryId, number), options);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<IssueEvent> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<IssueEvent> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueEvent> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<IssueEvent>(ApiUrls.IssuesEvents(owner, name), options);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueEvent> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<IssueEvent>(ApiUrls.IssuesEvents(repositoryId), options);
        }

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The event id</param>
        public IObservable<IssueEvent> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The event id</param>
        public IObservable<IssueEvent> Get(long repositoryId, int number)
        {
            return _client.Get(repositoryId, number).ToObservable();
        }
    }
}