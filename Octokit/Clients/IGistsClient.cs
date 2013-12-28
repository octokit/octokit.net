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
        /// Gets the list of all gists for the provided <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user the gists of whom are returned</param>
        /// <returns>A list with the gists</returns>
        Task<IReadOnlyList<Gist>> GetAllForUser(string user);

        /// <summary>
        /// Gets the list of all gists for the provided <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user the gists of whom are returned</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <returns>A list with the gists</returns>
        Task<IReadOnlyList<Gist>> GetAllForUser(string user, DateTime since);

        /// <summary>
        /// Gets the list of all gists for the authenticated user.
        /// If the user is not authenticated returns all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>A list with the gists</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Gist>> GetAllForCurrent();

        /// <summary>
        /// Gets the list of all gists for the authenticated user.
        /// If the user is not authenticated returns all public gists
        /// </summary>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>A list with the gists</returns>
        Task<IReadOnlyList<Gist>> GetAllForCurrent(DateTime since);

        /// <summary>
        /// Gets the list of all starred gists for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>A list with the starred gists</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Gist>> GetStarredForCurrent();

        /// <summary>
        /// Gets the list of all starred gists for the authenticated user.
        /// </summary>
        /// <remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>A list with the starred gists</returns>
        Task<IReadOnlyList<Gist>> GetStarredForCurrent(DateTime since);

        /// <summary>
        /// Get the list of all public gists.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>A list with the the public gists</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Gist>> GetPublic();

        /// <summary>
        /// Get the list of all public gists.
        /// </summary>
        /// <remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>A list with the the public gists</returns>
        Task<IReadOnlyList<Gist>> GetPublic(DateTime since);

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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unstar",
            Justification = "This is how it's called in GitHub API")]
        Task Unstar(string id);

        /// <summary>
        /// Deletes a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        Task Delete(string id);

    }
}