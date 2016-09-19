using System.Collections.Generic;
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
        /// <param name="number">The number of the issue</param>
        public Task<IReadOnlyList<Label>> GetAllForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task<IReadOnlyList<Label>> GetAllForIssue(long repositoryId, int number)
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
        public Task<IReadOnlyList<Label>> GetAllForIssue(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Label>(ApiUrls.IssueLabels(owner, name, number), options);
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
        public Task<IReadOnlyList<Label>> GetAllForIssue(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Label>(ApiUrls.IssueLabels(repositoryId, number), options);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public Task<IReadOnlyList<Label>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public Task<IReadOnlyList<Label>> GetAllForRepository(long repositoryId)
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
        public Task<IReadOnlyList<Label>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Label>(ApiUrls.Labels(owner, name), options);
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<Label>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Label>(ApiUrls.Labels(repositoryId), options);
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
        public Task<IReadOnlyList<Label>> GetAllForMilestone(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task<IReadOnlyList<Label>> GetAllForMilestone(long repositoryId, int number)
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
        public Task<IReadOnlyList<Label>> GetAllForMilestone(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Label>(ApiUrls.MilestoneLabels(owner, name, number), options);
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
        public Task<IReadOnlyList<Label>> GetAllForMilestone(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Label>(ApiUrls.MilestoneLabels(repositoryId, number), options);
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
        public Task<Label> Get(string owner, string name, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(labelName, "labelName");

            return ApiConnection.Get<Label>(ApiUrls.Label(owner, name, labelName));
        }

        /// <summary>
        /// Gets a single Label by name.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-a-single-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="labelName">The name of the label</param>
        public Task<Label> Get(long repositoryId, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, "labelName");

            return ApiConnection.Get<Label>(ApiUrls.Label(repositoryId, labelName));
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
        public Task Delete(string owner, string name, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(labelName, "labelName");

            return ApiConnection.Delete(ApiUrls.Label(owner, name, labelName));
        }

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#delete-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="labelName">The name of the label</param>
        public Task Delete(long repositoryId, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, "labelName");

            return ApiConnection.Delete(ApiUrls.Label(repositoryId, labelName));
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
        public Task<Label> Create(string owner, string name, NewLabel newLabel)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newLabel, "newLabel");

            return ApiConnection.Post<Label>(ApiUrls.Labels(owner, name), newLabel);
        }

        /// <summary>
        /// Creates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#create-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newLabel">The data for the label to be created</param>
        public Task<Label> Create(long repositoryId, NewLabel newLabel)
        {
            Ensure.ArgumentNotNull(newLabel, "newLabel");

            return ApiConnection.Post<Label>(ApiUrls.Labels(repositoryId), newLabel);
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
        public Task<Label> Update(string owner, string name, string labelName, LabelUpdate labelUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(labelName, "labelName");
            Ensure.ArgumentNotNull(labelUpdate, "labelUpdate");

            return ApiConnection.Post<Label>(ApiUrls.Label(owner, name, labelName), labelUpdate);
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
        public Task<Label> Update(long repositoryId, string labelName, LabelUpdate labelUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, "labelName");
            Ensure.ArgumentNotNull(labelUpdate, "labelUpdate");

            return ApiConnection.Post<Label>(ApiUrls.Label(repositoryId, labelName), labelUpdate);
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
        public Task<IReadOnlyList<Label>> AddToIssue(string owner, string name, int number, string[] labels)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(labels, "labels");

            return ApiConnection.Post<IReadOnlyList<Label>>(ApiUrls.IssueLabels(owner, name, number), labels);
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
        public Task<IReadOnlyList<Label>> AddToIssue(long repositoryId, int number, string[] labels)
        {
            Ensure.ArgumentNotNull(labels, "labels");

            return ApiConnection.Post<IReadOnlyList<Label>>(ApiUrls.IssueLabels(repositoryId, number), labels);
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
        public Task RemoveFromIssue(string owner, string name, int number, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(labelName, "labelName");

            return ApiConnection.Delete(ApiUrls.IssueLabel(owner, name, number, labelName));
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
        public Task RemoveFromIssue(long repositoryId, int number, string labelName)
        {
            Ensure.ArgumentNotNullOrEmptyString(labelName, "labelName");

            return ApiConnection.Delete(ApiUrls.IssueLabel(repositoryId, number, labelName));
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
        public Task<IReadOnlyList<Label>> ReplaceAllForIssue(string owner, string name, int number, string[] labels)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(labels, "labels");

            return ApiConnection.Put<IReadOnlyList<Label>>(ApiUrls.IssueLabels(owner, name, number), labels);
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
        public Task<IReadOnlyList<Label>> ReplaceAllForIssue(long repositoryId, int number, string[] labels)
        {
            Ensure.ArgumentNotNull(labels, "labels");

            return ApiConnection.Put<IReadOnlyList<Label>>(ApiUrls.IssueLabels(repositoryId, number), labels);
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
        public Task RemoveAllFromIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.IssueLabels(owner, name, number));
        }

        /// <summary>
        /// Removes all labels from an issue
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#remove-all-labels-from-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the issue</param>
        public Task RemoveAllFromIssue(long repositoryId, int number)
        {
            return ApiConnection.Delete(ApiUrls.IssueLabels(repositoryId, number));
        }
    }
}
