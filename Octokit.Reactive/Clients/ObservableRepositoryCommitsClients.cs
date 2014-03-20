using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableRepositoryCommitsClient : IObservableRepositoryCommitsClient
    {
        readonly IGitHubClient _client;

        public ObservableRepositoryCommitsClient(IGitHubClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <returns></returns>
        public IObservable<CompareResult> Compare(string owner, string name, string @base, string head)
        {
            return _client.Repository.Commits.Compare(owner, name, @base, head).ToObservable();
        }
    }
}
