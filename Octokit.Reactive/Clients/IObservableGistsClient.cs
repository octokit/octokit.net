    using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableGistsClient
    {
        IObservableGistCommentsClient Comment { get; set; }

        /// <summary>
        /// Gets a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#get-a-single-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<Gist> Get(string id);

        /// <summary>
        /// Gets the list of all gists for the provided <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user the gists of whom are returned</param>
        /// <returns>IObservable{Gist}</returns>
        IObservable<Gist> GetAllForUser(string user);

        /// <summary>
        /// Gets the list of all gists for the authenticated user.
        /// If the user is not authenticated returns all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>IObservable{Gist}</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<Gist> GetAllForCurrent();

    }
}