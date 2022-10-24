using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's gitignore APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/gitignore">GitIgnore API documentation</a> for more details.
    /// </remarks>
    public class ObservableGitIgnoreClient : IObservableGitIgnoreClient
    {
        private readonly IGitIgnoreClient _client;

        public ObservableGitIgnoreClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.GitIgnore;
        }

        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>An observable list of gitignore template names.</returns>
        public IObservable<string> GetAllGitIgnoreTemplates()
        {
            return _client.GetAllGitIgnoreTemplates().ToObservable().SelectMany(t => t);
        }

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName">Returns the template source for the given template</param>
        public IObservable<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName)
        {
            return _client.GetGitIgnoreTemplate(templateName).ToObservable();
        }
    }
}
