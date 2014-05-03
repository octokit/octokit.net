using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Gists API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/gists/">Gists API documentation</a> for more information.
    /// </remarks>
    public interface IGistsClient
    {
        IGistCommentsClient Comment { get; set; }

        /// <summary>
        /// Gets a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#get-a-single-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<Gist> Get(string id);

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        Task<IReadOnlyList<Gist>> GetAll();

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        Task<IReadOnlyList<Gist>> GetAll(DateTimeOffset since);

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        Task<IReadOnlyList<Gist>> GetAllPublic();

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        Task<IReadOnlyList<Gist>> GetAllPublic(DateTimeOffset since);

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        Task<IReadOnlyList<Gist>> GetAllStarred();

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        Task<IReadOnlyList<Gist>> GetAllStarred(DateTimeOffset since);

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        Task<IReadOnlyList<Gist>> GetAllForUser(string user);

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        Task<IReadOnlyList<Gist>> GetAllForUser(string user, DateTimeOffset since);

        /// <summary>
        /// Creates a new gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#create-a-gist
        /// </remarks>
        /// <param name="newGist">The new gist to create</param>
        Task<Gist> Create(NewGist newGist);

        /// <summary>
        /// Creates a fork of a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#fork-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist to fork</param>
        Task<Gist> Fork(string id);

        /// <summary>
        /// Edits a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="gistUpdate">The update to the gist</param>
        Task<Gist> Edit(string id, GistUpdate gistUpdate);

        /// <summary>
        /// Deletes a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        Task Delete(string id);

        /// <summary>
        /// Stars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#star-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        Task Star(string id);

        /// <summary>
        /// Unstars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#unstar-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unstar")]
        Task Unstar(string id);

        /// <summary>
        /// Checks if the gist is starred
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#check-if-a-gist-is-starred
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        Task<bool> IsStarred(string id);
    }
}