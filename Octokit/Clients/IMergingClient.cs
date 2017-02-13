﻿using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Merging API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/merging/">Git Merging API documentation</a> for more information.
    /// </remarks>
    public interface IMergingClient
    {
        /// <summary>
        /// Create a merge for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/merging/#perform-a-merge
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="merge">The merge to create</param>
        /// <returns></returns>
        Task<Merge> Create(string owner, string name, NewMerge merge);

        /// <summary>
        /// Create a merge for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/merging/#perform-a-merge
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="merge">The merge to create</param>
        /// <returns></returns>
        Task<Merge> Create(long repositoryId, NewMerge merge);
    }
}