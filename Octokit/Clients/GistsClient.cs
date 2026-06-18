using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Gists API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/gists/">Gists API documentation</a> for more information.
    /// </remarks>
    public class GistsClient : ApiClient, IGistsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Gists API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public GistsClient(IApiConnection apiConnection) :
            base(apiConnection)
        {
            Comment = new GistCommentsClient(apiConnection);
        }

        public IGistCommentsClient Comment { get; set; }

        /// <summary>
        /// Gets a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#get-a-single-gist
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/{gist_id}")]
        public Task<Gist> Get(string gistId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Get<Gist>(ApiUrls.Gist(gistId), cancellationToken);
        }

        /// <summary>
        /// Creates a new gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#create-a-gist
        /// </remarks>
        /// <param name="newGist">The new gist to create</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("POST", "/gists")]
        public Task<Gist> Create(NewGist newGist, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newGist, nameof(newGist));

            //Required to create anonymous object to match signature of files hash.
            // Allowing the serializer to handle Dictionary<string,NewGistFile>
            // will fail to match.
            var filesAsJsonObject = new JsonObject();
            foreach (var kvp in newGist.Files)
            {
                filesAsJsonObject.Add(kvp.Key, new { Content = kvp.Value });
            }

            var gist = new
            {
                Description = newGist.Description,
                Public = newGist.Public,
                Files = filesAsJsonObject
            };

            return ApiConnection.Post<Gist>(ApiUrls.Gist(), gist, cancellationToken);
        }

        /// <summary>
        /// Creates a fork of a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#fork-a-gist
        /// </remarks>
        /// <param name="gistId">The id of the gist to fork</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("POST", "/gists/{gist_id}/forks")]
        public Task<Gist> Fork(string gistId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Post<Gist>(ApiUrls.ForkGist(gistId), new object(), cancellationToken);
        }

        /// <summary>
        /// Deletes a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/gists/{gist_id}")]
        public Task Delete(string gistId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));

            return ApiConnection.Delete(ApiUrls.Gist(gistId), cancellationToken);
        }

        /// <summary>
        /// List the authenticated user's gists or if called anonymously,
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists")]
        public Task<IReadOnlyList<Gist>> GetAll(CancellationToken cancellationToken = default)
        {
            return GetAll(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user's gists or if called anonymously,
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists")]
        public Task<IReadOnlyList<Gist>> GetAll(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Gist>(ApiUrls.Gist(), options, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user's gists or if called anonymously,
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists")]
        public Task<IReadOnlyList<Gist>> GetAll(DateTimeOffset since, CancellationToken cancellationToken = default)
        {
            return GetAll(since, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user's gists or if called anonymously,
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists")]
        public Task<IReadOnlyList<Gist>> GetAll(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return ApiConnection.GetAll<Gist>(ApiUrls.Gist(), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/public")]
        public Task<IReadOnlyList<Gist>> GetAllPublic(CancellationToken cancellationToken = default)
        {
            return GetAllPublic(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/public")]
        public Task<IReadOnlyList<Gist>> GetAllPublic(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Gist>(ApiUrls.PublicGists(), options, cancellationToken);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/public")]
        public Task<IReadOnlyList<Gist>> GetAllPublic(DateTimeOffset since, CancellationToken cancellationToken = default)
        {
            return GetAllPublic(since, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/public")]
        public Task<IReadOnlyList<Gist>> GetAllPublic(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return ApiConnection.GetAll<Gist>(ApiUrls.PublicGists(), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user's starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/starred")]
        public Task<IReadOnlyList<Gist>> GetAllStarred(CancellationToken cancellationToken = default)
        {
            return GetAllStarred(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user's starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/starred")]
        public Task<IReadOnlyList<Gist>> GetAllStarred(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Gist>(ApiUrls.StarredGists(), options, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user's starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/starred")]
        public Task<IReadOnlyList<Gist>> GetAllStarred(DateTimeOffset since, CancellationToken cancellationToken = default)
        {
            return GetAllStarred(since, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user's starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/starred")]
        public Task<IReadOnlyList<Gist>> GetAllStarred(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return ApiConnection.GetAll<Gist>(ApiUrls.StarredGists(), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/users/{username}/gists")]
        public Task<IReadOnlyList<Gist>> GetAllForUser(string user, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/users/{username}/gists")]
        public Task<IReadOnlyList<Gist>> GetAllForUser(string user, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Gist>(ApiUrls.UsersGists(user), options, cancellationToken);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/users/{username}/gists")]
        public Task<IReadOnlyList<Gist>> GetAllForUser(string user, DateTimeOffset since, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, since, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/users/{username}/gists")]
        public Task<IReadOnlyList<Gist>> GetAllForUser(string user, DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return ApiConnection.GetAll<Gist>(ApiUrls.UsersGists(user), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// List gist commits
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-commits
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/{gist_id}/commits")]
        public Task<IReadOnlyList<GistHistory>> GetAllCommits(string gistId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));

            return GetAllCommits(gistId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List gist commits
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-commits
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/{gist_id}/commits")]
        public Task<IReadOnlyList<GistHistory>> GetAllCommits(string gistId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<GistHistory>(ApiUrls.GistCommits(gistId), options, cancellationToken);
        }

        /// <summary>
        /// List gist forks
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-forks
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/{gist_id}/forks")]
        public Task<IReadOnlyList<GistFork>> GetAllForks(string gistId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));

            return GetAllForks(gistId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List gist forks
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-forks
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/{gist_id}/forks")]
        public Task<IReadOnlyList<GistFork>> GetAllForks(string gistId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<GistFork>(ApiUrls.ForkGist(gistId), options, cancellationToken);
        }

        /// <summary>
        /// Edits a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="gistUpdate">The update to the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("PATCH", "/gists/{gist_id}")]
        public Task<Gist> Edit(string gistId, GistUpdate gistUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));
            Ensure.ArgumentNotNull(gistUpdate, nameof(gistUpdate));

            var filesAsJsonObject = new JsonObject();
            foreach (var kvp in gistUpdate.Files)
            {
                filesAsJsonObject.Add(kvp.Key, new { Content = kvp.Value.Content, Filename = kvp.Value.NewFileName });
            }

            var gist = new
            {
                Description = gistUpdate.Description,
                Files = filesAsJsonObject
            };

            return ApiConnection.Patch<Gist>(ApiUrls.Gist(gistId), gist, cancellationToken);
        }

        /// <summary>
        /// Stars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#star-a-gist
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("PUT", "/gists/{gist_id}/star")]
        public Task Star(string gistId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));

            return ApiConnection.Put(ApiUrls.StarGist(gistId), cancellationToken);
        }

        /// <summary>
        /// Unstars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#unstar-a-gist
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/gists/{gist_id}/star")]
        public Task Unstar(string gistId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));

            return ApiConnection.Delete(ApiUrls.StarGist(gistId), cancellationToken);
        }

        /// <summary>
        /// Checks if the gist is starred
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#check-if-a-gist-is-starred
        /// </remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/gists/{gist_id}/star")]
        public async Task<bool> IsStarred(string gistId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));

            try
            {
                var response = await Connection.Get<object>(ApiUrls.StarGist(gistId), null, null, cancellationToken).ConfigureAwait(false);
                return response.HttpResponse.IsTrue();
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
