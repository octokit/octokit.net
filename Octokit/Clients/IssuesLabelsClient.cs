using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Labels API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/labels/">Issue Labels API documentation</a> for more information.
    /// </remarks>
    public class IssuesLabelsClient : ApiClient, IIssuesLabelsClient
    {
        public IssuesLabelsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{issue_number}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForIssue(string owner, string name, long issueNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForIssue(owner, name, issueNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForIssue(long repositoryId, long issueNumber, CancellationToken cancellationToken = default)
        {
            return GetAllForIssue(repositoryId, issueNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{issue_number}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForIssue(string owner, string name, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Label>(ApiUrls.IssueLabels(owner, name, issueNumber), options, cancellationToken);
        }

        /// <summary>
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForIssue(long repositoryId, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Label>(ApiUrls.IssueLabels(repositoryId, issueNumber), options, cancellationToken);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Label>(ApiUrls.Labels(owner, name), options, cancellationToken);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Label>(ApiUrls.Labels(repositoryId), options, cancellationToken);
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="milestoneNumber">The number of the milestone</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/milestones/{milestone_number}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForMilestone(string owner, string name, int milestoneNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForMilestone(owner, name, milestoneNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="milestoneNumber">The number of the milestone</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/milestones/{milestone_number}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForMilestone(long repositoryId, int milestoneNumber, CancellationToken cancellationToken = default)
        {
            return GetAllForMilestone(repositoryId, milestoneNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="milestoneNumber">The number of the milestone</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/milestones/{milestone_number}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForMilestone(string owner, string name, int milestoneNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Label>(ApiUrls.MilestoneLabels(owner, name, milestoneNumber), options, cancellationToken);
        }

        /// <summary>
        /// Gets labels for every issue in a milestone
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-labels-for-every-issue-in-a-milestone">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="milestoneNumber">The number of the milestone</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/milestones/{milestone_number}/labels")]
        public Task<IReadOnlyList<Label>> GetAllForMilestone(long repositoryId, int milestoneNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Label>(ApiUrls.MilestoneLabels(repositoryId, milestoneNumber), options, cancellationToken);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/labels/{name}")]
        public Task<Label> Get(string owner, string name, string labelName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return ApiConnection.Get<Label>(ApiUrls.Label(owner, name, labelName), null, cancellationToken);
        }

        /// <summary>
        /// Gets a single Label by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-a-single-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="labelName">The name of the label</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/labels/{name}")]
        public Task<Label> Get(long repositoryId, string labelName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return ApiConnection.Get<Label>(ApiUrls.Label(repositoryId, labelName), null, cancellationToken);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/labels/{name}")]
        public Task Delete(string owner, string name, string labelName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return ApiConnection.Delete(ApiUrls.Label(owner, name, labelName), cancellationToken);
        }

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#delete-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="labelName">The name of the label</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/repositories/{id}/labels/{name}")]
        public Task Delete(long repositoryId, string labelName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return ApiConnection.Delete(ApiUrls.Label(repositoryId, labelName), cancellationToken);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/labels")]
        public Task<Label> Create(string owner, string name, NewLabel newLabel, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newLabel, nameof(newLabel));

            return ApiConnection.Post<Label>(ApiUrls.Labels(owner, name), newLabel, cancellationToken);
        }

        /// <summary>
        /// Creates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#create-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newLabel">The data for the label to be created</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("POST", "/repositories/{id}/labels/{name}")]
        public Task<Label> Create(long repositoryId, NewLabel newLabel, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newLabel, nameof(newLabel));

            return ApiConnection.Post<Label>(ApiUrls.Labels(repositoryId), newLabel, cancellationToken);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/labels/{name}")]
        public Task<Label> Update(string owner, string name, string labelName, LabelUpdate labelUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));
            Ensure.ArgumentNotNull(labelUpdate, nameof(labelUpdate));

            // BUG: this should be a PATCH instead of POST

            return ApiConnection.Post<Label>(ApiUrls.Label(owner, name, labelName), labelUpdate, cancellationToken);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("PATCH", "/repositories/{id}/labels/{name}")]
        public Task<Label> Update(long repositoryId, string labelName, LabelUpdate labelUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));
            Ensure.ArgumentNotNull(labelUpdate, nameof(labelUpdate));

            // BUG: this should be a PATCH instead of POST

            return ApiConnection.Post<Label>(ApiUrls.Label(repositoryId, labelName), labelUpdate, cancellationToken);
        }

        /// <summary>
        /// Adds a label to an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#add-labels-to-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="labels">The names of the labels to add</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues/{issue_number}/labels")]
        public Task<IReadOnlyList<Label>> AddToIssue(string owner, string name, long issueNumber, string[] labels, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(labels, nameof(labels));

            return ApiConnection.Post<IReadOnlyList<Label>>(ApiUrls.IssueLabels(owner, name, issueNumber), labels, cancellationToken);
        }

        /// <summary>
        /// Adds a label to an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#add-labels-to-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="labels">The names of the labels to add</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("POST", "/repositories/{id}/issues/{number}/labels")]
        public Task<IReadOnlyList<Label>> AddToIssue(long repositoryId, long issueNumber, string[] labels, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(labels, nameof(labels));

            return ApiConnection.Post<IReadOnlyList<Label>>(ApiUrls.IssueLabels(repositoryId, issueNumber), labels, cancellationToken);
        }

        /// <summary>
        /// Removes a label from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-a-label-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="labelName">The name of the label to remove</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/issues/{issue_number}/labels")]
        public Task<IReadOnlyList<Label>> RemoveFromIssue(string owner, string name, long issueNumber, string labelName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return ApiConnection.Delete<IReadOnlyList<Label>>(ApiUrls.IssueLabel(owner, name, issueNumber, labelName), new object(), cancellationToken);
        }

        /// <summary>
        /// Removes a label from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-a-label-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="labelName">The name of the label to remove</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/repositories/{id}/issues/{number}/labels")]
        public Task<IReadOnlyList<Label>> RemoveFromIssue(long repositoryId, long issueNumber, string labelName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, nameof(labelName));

            return ApiConnection.Delete<IReadOnlyList<Label>>(ApiUrls.IssueLabel(repositoryId, issueNumber, labelName), new object(), cancellationToken);
        }

        /// <summary>
        /// Replaces all labels on the specified issues with the provided labels
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#replace-all-labels-for-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="labels">The names of the labels to set</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/issues/{issue_number}/labels")]
        public Task<IReadOnlyList<Label>> ReplaceAllForIssue(string owner, string name, long issueNumber, string[] labels, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(labels, nameof(labels));

            return ApiConnection.Put<IReadOnlyList<Label>>(ApiUrls.IssueLabels(owner, name, issueNumber), labels, cancellationToken);
        }

        /// <summary>
        /// Replaces all labels on the specified issues with the provided labels
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#replace-all-labels-for-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="labels">The names of the labels to set</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("PUT", "/repositories/{id}/issues/{number}/labels")]
        public Task<IReadOnlyList<Label>> ReplaceAllForIssue(long repositoryId, long issueNumber, string[] labels, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(labels, nameof(labels));

            return ApiConnection.Put<IReadOnlyList<Label>>(ApiUrls.IssueLabels(repositoryId, issueNumber), labels, cancellationToken);
        }

        /// <summary>
        /// Removes all labels from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-all-labels-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/issues/{issue_number}/labels")]
        public Task RemoveAllFromIssue(string owner, string name, long issueNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.IssueLabels(owner, name, issueNumber), cancellationToken);
        }

        /// <summary>
        /// Removes all labels from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-all-labels-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/repositories/{id}/issues/{number}/labels")]
        public Task RemoveAllFromIssue(long repositoryId, long issueNumber, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Delete(ApiUrls.IssueLabels(repositoryId, issueNumber), cancellationToken);
        }
    }
}
