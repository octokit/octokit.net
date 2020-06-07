using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Labels API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/labels/">Issue Labels API documentation</a> for more information.
    /// </remarks>
    public class ObservableIssuesLabelsClient : IObservableIssuesLabelsClient
    {
        readonly IIssuesLabelsClient _client;
        readonly IConnection _connection;

        public ObservableIssuesLabelsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _client = client.Issue.Labels;
        }

        /// <summary>
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        public IObservable<Label> GetAllForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForIssue(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the issue</param>
        public IObservable<Label> GetAllForIssue(long repositoryId, int number)
        {
            return GetAllForIssue(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Label> GetAllForIssue(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.IssueLabels(owner, name, number), options);
        }

        /// <summary>
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Label> GetAllForIssue(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.IssueLabels(repositoryId, number), options);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<Label> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<Label> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Label> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.Labels(owner, name), options);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Label> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.Labels(repositoryId), options);
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the milestone</param>
        public IObservable<Label> GetAllForMilestone(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForMilestone(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the milestone</param>
        public IObservable<Label> GetAllForMilestone(long repositoryId, int number)
        {
            return GetAllForMilestone(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the milestone</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Label> GetAllForMilestone(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.MilestoneLabels(owner, name, number), options);
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the milestone</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Label> GetAllForMilestone(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.MilestoneLabels(repositoryId, number), options);
        }

        /// <summary>
        /// Gets a single Label by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-a-single-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="labelName">The name of the label</param>
        public IObservable<Label> Get(string owner, string name, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return _client.Get(owner, name, labelName).ToObservable();
        }

        /// <summary>
        /// Gets a single Label by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-a-single-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="labelName">The name of the label</param>
        public IObservable<Label> Get(long repositoryId, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return _client.Get(repositoryId, labelName).ToObservable();
        }

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#delete-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="labelName">The name of the label</param>
        public IObservable<Unit> Delete(string owner, string name, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return _client.Delete(owner, name, labelName).ToObservable();
        }

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#delete-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="labelName">The name of the label</param>
        public IObservable<Unit> Delete(long repositoryId, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return _client.Delete(repositoryId, labelName).ToObservable();
        }

        /// <summary>
        /// Creates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#create-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newLabel">The data for the label to be created</param>
        public IObservable<Label> Create(string owner, string name, NewLabel newLabel)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newLabel, nameof(newLabel));

            return _client.Create(owner, name, newLabel).ToObservable();
        }

        /// <summary>
        /// Creates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#create-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newLabel">The data for the label to be created</param>
        public IObservable<Label> Create(long repositoryId, NewLabel newLabel)
        {
            Ensure.ArgumentNotNull(newLabel, nameof(newLabel));

            return _client.Create(repositoryId, newLabel).ToObservable();
        }

        /// <summary>
        /// Updates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#update-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="labelName">The name of the label</param>
        /// <param name="labelUpdate">The data for the label to be updated</param>
        public IObservable<Label> Update(string owner, string name, string labelName, LabelUpdate labelUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));
            Ensure.ArgumentNotNull(labelUpdate, nameof(labelUpdate));

            return _client.Update(owner, name, labelName, labelUpdate).ToObservable();
        }

        /// <summary>
        /// Updates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#update-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="labelName">The name of the label</param>
        /// <param name="labelUpdate">The data for the label to be updated</param>
        public IObservable<Label> Update(long repositoryId, string labelName, LabelUpdate labelUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));
            Ensure.ArgumentNotNull(labelUpdate, nameof(labelUpdate));

            return _client.Update(repositoryId, labelName, labelUpdate).ToObservable();
        }

        /// <summary>
        /// Adds a label to an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#add-labels-to-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="labels">The names of the labels to add</param>
        public IObservable<Label> AddToIssue(string owner, string name, int number, string[] labels)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(labels, nameof(labels));

            return _client.AddToIssue(owner, name, number, labels)
                .ToObservable()
                .SelectMany(x => x); // HACK: POST is not compatible with GetAndFlattenPages
        }

        /// <summary>
        /// Adds a label to an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#add-labels-to-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="labels">The names of the labels to add</param>
        public IObservable<Label> AddToIssue(long repositoryId, int number, string[] labels)
        {
            Ensure.ArgumentNotNull(labels, nameof(labels));

            return _client.AddToIssue(repositoryId, number, labels)
                .ToObservable()
                .SelectMany(x => x); // HACK: POST is not compatible with GetAndFlattenPages
        }

        /// <summary>
        /// Removes a label from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-a-label-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="labelName">The name of the label to remove</param>
        public IObservable<Label> RemoveFromIssue(string owner, string name, int number, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return _client.RemoveFromIssue(owner, name, number, labelName)
                .ToObservable()
                .SelectMany(x => x); // HACK: DELETE is not compatible with GetAndFlattenPages
        }

        /// <summary>
        /// Removes a label from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-a-label-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="labelName">The name of the label to remove</param>
        public IObservable<Label> RemoveFromIssue(long repositoryId, int number, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return _client.RemoveFromIssue(repositoryId, number, labelName)
                .ToObservable()
                .SelectMany(x => x); // HACK: DELETE is not compatible with GetAndFlattenPages
        }

        /// <summary>
        /// Replaces all labels on the specified issues with the provided labels
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#replace-all-labels-for-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="labels">The names of the labels to set</param>
        public IObservable<Label> ReplaceAllForIssue(string owner, string name, int number, string[] labels)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(labels, nameof(labels));

            return _client.ReplaceAllForIssue(owner, name, number, labels)
                .ToObservable()
                .SelectMany(x => x);  // HACK: PUT is not compatible with GetAndFlattenPages
        }

        /// <summary>
        /// Replaces all labels on the specified issues with the provided labels
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#replace-all-labels-for-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="labels">The names of the labels to set</param>
        public IObservable<Label> ReplaceAllForIssue(long repositoryId, int number, string[] labels)
        {
            Ensure.ArgumentNotNull(labels, nameof(labels));

            return _client.ReplaceAllForIssue(repositoryId, number, labels)
                .ToObservable()
                .SelectMany(x => x);  // HACK: PUT is not compatible with GetAndFlattenPages
        }

        /// <summary>
        /// Removes all labels from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-all-labels-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        public IObservable<Unit> RemoveAllFromIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.RemoveAllFromIssue(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Removes all labels from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-all-labels-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the issue</param>
        public IObservable<Unit> RemoveAllFromIssue(long repositoryId, int number)
        {
            return _client.RemoveAllFromIssue(repositoryId, number).ToObservable();
        }
    }
}
