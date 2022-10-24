﻿using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's meta APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/meta">Meta API documentation</a> for more details.
    /// </remarks>
    public interface IObservableMetaClient
    {
        /// <summary>
        /// Retrieves information about GitHub.com, the service or a GitHub Enterprise installation.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="Meta"/> containing metadata about the GitHub instance.</returns>
        IObservable<Meta> GetMetadata();
    }
}
