using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Request Review Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/comments/">Review Comments API documentation</a> for more information.
    /// </remarks>
    public class PullRequestReviewCommentsClient : ApiClient, IPullRequestReviewCommentsClient
    {
        public PullRequestReviewCommentsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAll(long repositoryId, int number)
        {
            return GetAll(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAll(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(owner, name, number), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAll(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(repositoryId, number), options);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, new PullRequestReviewCommentRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, new PullRequestReviewCommentRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return GetAllForRepository(owner, name, new PullRequestReviewCommentRequest(), options);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return GetAllForRepository(repositoryId, new PullRequestReviewCommentRequest(), options);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, PullRequestReviewCommentRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<PullRequestReviewComment>(ApiUrls.PullRequestReviewCommentsRepository(owner, name), request.ToParametersDictionary(), AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, PullRequestReviewCommentRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<PullRequestReviewComment>(ApiUrls.PullRequestReviewCommentsRepository(repositoryId), request.ToParametersDictionary(), AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        public Task<PullRequestReviewComment> GetComment(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<PullRequestReviewComment>(ApiUrls.PullRequestReviewComment(owner, name, number), new Dictionary<string, string>(), AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        public Task<PullRequestReviewComment> GetComment(long repositoryId, int number)
        {
            return ApiConnection.Get<PullRequestReviewComment>(ApiUrls.PullRequestReviewComment(repositoryId, number));
        }

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="comment">The comment</param>
        public async Task<PullRequestReviewComment> Create(string owner, string name, int number, PullRequestReviewCommentCreate comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(comment, "comment");

            var endpoint = ApiUrls.PullRequestReviewComments(owner, name, number);
            var response = await ApiConnection.Connection.Post<PullRequestReviewComment>(endpoint, comment, null, null).ConfigureAwait(false);

            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="comment">The comment</param>
        public async Task<PullRequestReviewComment> Create(long repositoryId, int number, PullRequestReviewCommentCreate comment)
        {
            Ensure.ArgumentNotNull(comment, "comment");

            var endpoint = ApiUrls.PullRequestReviewComments(repositoryId, number);
            var response = await ApiConnection.Connection.Post<PullRequestReviewComment>(endpoint, comment, null, null).ConfigureAwait(false);

            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="comment">The comment</param>
        public async Task<PullRequestReviewComment> CreateReply(string owner, string name, int number, PullRequestReviewCommentReplyCreate comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(comment, "comment");

            var endpoint = ApiUrls.PullRequestReviewComments(owner, name, number);
            var response = await ApiConnection.Connection.Post<PullRequestReviewComment>(endpoint, comment, null, null).ConfigureAwait(false);

            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="comment">The comment</param>
        public async Task<PullRequestReviewComment> CreateReply(long repositoryId, int number, PullRequestReviewCommentReplyCreate comment)
        {
            Ensure.ArgumentNotNull(comment, "comment");

            var endpoint = ApiUrls.PullRequestReviewComments(repositoryId, number);
            var response = await ApiConnection.Connection.Post<PullRequestReviewComment>(endpoint, comment, null, null).ConfigureAwait(false);

            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="comment">The edited comment</param>
        public Task<PullRequestReviewComment> Edit(string owner, string name, int number, PullRequestReviewCommentEdit comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(comment, "comment");

            return ApiConnection.Patch<PullRequestReviewComment>(ApiUrls.PullRequestReviewComment(owner, name, number), comment);
        }

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="comment">The edited comment</param>
        public Task<PullRequestReviewComment> Edit(long repositoryId, int number, PullRequestReviewCommentEdit comment)
        {
            Ensure.ArgumentNotNull(comment, "comment");

            return ApiConnection.Patch<PullRequestReviewComment>(ApiUrls.PullRequestReviewComment(repositoryId, number), comment);
        }

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        public Task Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.PullRequestReviewComment(owner, name, number));
        }

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        public Task Delete(long repositoryId, int number)
        {
            return ApiConnection.Delete(ApiUrls.PullRequestReviewComment(repositoryId, number));
        }
    }
}
