using System;
using System.Collections.Generic;

namespace Octokit.Reactive
{
    public interface IObservableStatisticsClient
    {
        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>A list of <see cref="Contributor"/></returns>
        IObservable<IEnumerable<Contributor>> GetContributors(string owner, string repositoryName);
    }
}