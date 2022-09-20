using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's gitignore APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/gitignore">GitIgnore API documentation</a> for more details.
    /// </remarks>
    public interface IObservableGitIgnoreClient
    {
        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>An observable list of gitignore template names.</returns>
        IObservable<string> GetAllGitIgnoreTemplates();

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName">Returns the template source for the given template</param>
        IObservable<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName);
    }
}
