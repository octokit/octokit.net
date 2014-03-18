using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableRepositoryContentsClient : IObservableRepositoryContentsClient
    {
        readonly IGitHubClient _client;

        public ObservableRepositoryContentsClient(IGitHubClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Returns the HTML rendered README.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Content.GetReadme(owner, name).ToObservable();
        }

        /// <summary>
        /// Returns just the HTML portion of the README without the surrounding HTML document. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<string> GetReadmeHtml(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Content.GetReadmeHtml(owner, name).ToObservable();
        }
    }
}