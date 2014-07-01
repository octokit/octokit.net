using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableIssuesLabelsClient : IObservableIssuesLabelsClient
    {
        readonly IIssuesLabelsClient _client;
        readonly IConnection _connection;

        public ObservableIssuesLabelsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

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
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <returns>The list of labels</returns>
        public IObservable<Label> GetForIssue(string owner, string repo, int number)
        {
            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.IssueLabels(owner, repo, number));
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <returns>The list of labels</returns>
        public IObservable<Label> GetForRepository(string owner, string repo)
        {
            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.Labels(owner, repo));
        }

        /// <summary>
        /// Gets a single Label by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-a-single-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="name">The name of the label</param>
        /// <returns>The label</returns>
        public IObservable<Label> Get(string owner, string repo, string name)
        {
            return _client.Get(owner, repo, name).ToObservable();
        }

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#delete-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="name">The name of the label</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string repo, string name)
        {
            return _client.Delete(owner, repo, name).ToObservable();
        }

        /// <summary>
        /// Creates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#create-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="newLabel">The data for the label to be created</param>
        /// <returns>The created label</returns>
        public IObservable<Label> Create(string owner, string repo, NewLabel newLabel)
        {
            return _client.Create(owner, repo, newLabel).ToObservable();
        }

        /// <summary>
        /// Updates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#update-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="name">The name of the label</param>
        /// <param name="labelUpdate">The data for the label to be updated</param>
        /// <returns>The updated label</returns>
        public IObservable<Label> Update(string owner, string repo, string name, LabelUpdate labelUpdate)
        {
            return _client.Update(owner, repo, name, labelUpdate).ToObservable();
        }

        /// <summary>
        /// Adds a label to an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#add-labels-to-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="labels">The names of the labels to add</param>
        /// <returns></returns>
        public IObservable<Label> AddToIssue(string owner, string repo, int number, string[] labels)
        {
            return _client.AddToIssue(owner, repo, number, labels)
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
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="label">The name of the label to remove</param>
        /// <returns></returns>
        public IObservable<Unit> RemoveFromIssue(string owner, string repo, int number, string label)
        {
            return _client.RemoveFromIssue(owner, repo, number, label).ToObservable();
        }

        /// <summary>
        /// Replaces all labels on the specified issues with the provided labels
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#replace-all-labels-for-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="labels">The names of the labels to set</param>
        /// <returns></returns>
        public IObservable<Label> ReplaceAllForIssue(string owner, string repo, int number, string[] labels)
        {
            return _client.ReplaceAllForIssue(owner, repo, number, labels)
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
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <returns></returns>
        public IObservable<Unit> RemoveAllFromIssue(string owner, string repo, int number)
        {
            return _client.RemoveAllFromIssue(owner, repo, number).ToObservable();
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The number of the milestone</param>
        /// <returns></returns>
        public IObservable<Label> GetForMilestone(string owner, string repo, int number)
        {
            return _connection.GetAndFlattenAllPages<Label>(ApiUrls.MilestoneLabels(owner, repo, number));
        }
    }
}