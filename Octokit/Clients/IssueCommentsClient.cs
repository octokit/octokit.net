﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/comments/">Issue Comments API documentation</a> for more information.
    /// </remarks>
    public class IssueCommentsClient : ApiClient, IIssueCommentsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Issue Comments API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public IssueCommentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The issue comment id</param>
        /// <returns></returns>
        public Task<IssueComment> Get(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<IssueComment>(ApiUrls.IssueComment(owner, name, id), null, AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<IssueComment>(ApiUrls.IssueComments(owner, name), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForIssue(owner, name, number, ApiOptions.None);
        }
        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<IssueComment>(ApiUrls.IssueComments(owner, name, number), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="newComment">The new comment to add to the issue</param>
        /// <returns></returns>
        public Task<IssueComment> Create(string owner, string name, int number, string newComment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newComment, "newComment");

            return ApiConnection.Post<IssueComment>(ApiUrls.IssueComments(owner, name, number), new BodyWrapper(newComment));
        }

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <returns></returns>
        public Task<IssueComment> Update(string owner, string name, int id, string commentUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(commentUpdate, "commentUpdate");

            return ApiConnection.Patch<IssueComment>(ApiUrls.IssueComment(owner, name, id), new BodyWrapper(commentUpdate));
        }

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The comment id</param>
        /// <returns></returns>
        public Task Delete(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.IssueComment(owner, name, id));
        }
    }
}
