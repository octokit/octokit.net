﻿using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Traffic API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/traffic/">Repository Traffic API documentation</a> for more information.
    /// </remarks>
    public interface IObservableRepositoryTrafficClient
    {
        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [Obsolete("Please use GetAllReferrers instead")]
        IObservable<RepositoryTrafficReferrer> GetReferrers(string owner, string name);

        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        [Obsolete("Please use GetAllReferrers instead")]
        IObservable<RepositoryTrafficReferrer> GetReferrers(long repositoryId);

        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<RepositoryTrafficReferrer> GetAllReferrers(string owner, string name);

        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        IObservable<RepositoryTrafficReferrer> GetAllReferrers(long repositoryId);

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [Obsolete("Please use GetAllPaths instead")]
        IObservable<RepositoryTrafficPath> GetPaths(string owner, string name);

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        [Obsolete("Please use GetAllPaths instead")]
        IObservable<RepositoryTrafficPath> GetPaths(long repositoryId);

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<RepositoryTrafficPath> GetAllPaths(string owner, string name);

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        IObservable<RepositoryTrafficPath> GetAllPaths(long repositoryId);

        /// <summary>
        /// Get the total number of views and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#views</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        IObservable<RepositoryTrafficViewSummary> GetViews(string owner, string name, RepositoryTrafficRequest per);

        /// <summary>
        /// Get the total number of views and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#views</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        IObservable<RepositoryTrafficViewSummary> GetViews(long repositoryId, RepositoryTrafficRequest per);

        /// <summary>
        /// Get the total number of clones and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#clones</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        IObservable<RepositoryTrafficCloneSummary> GetClones(string owner, string name, RepositoryTrafficRequest per);

        /// <summary>
        /// Get the total number of clones and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#clones</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        IObservable<RepositoryTrafficCloneSummary> GetClones(long repositoryId, RepositoryTrafficRequest per);
    }
}
