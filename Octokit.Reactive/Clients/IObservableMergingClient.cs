using System;

namespace Octokit.Reactive
{
    public interface IObservableMergingClient
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
        IObservable<Merge> Create(string owner, string name, NewMerge merge);
    }
}