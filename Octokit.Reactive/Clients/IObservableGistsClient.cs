using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive.Clients
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
    }
}