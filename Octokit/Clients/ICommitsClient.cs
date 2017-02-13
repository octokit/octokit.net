using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Commits API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/commits/">Git Commits API documentation</a> for more information.
    /// </remarks>
    public interface ICommitsClient
    {
        /// <summary>
        /// Gets a commit for a given repository by sha reference
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#get-a-commit
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">Tha sha reference of the commit</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<Commit> Get(string owner, string name, string reference);

        /// <summary>
        /// Gets a commit for a given repository by sha reference
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#get-a-commit
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">Tha sha reference of the commit</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<Commit> Get(long repositoryId, string reference);

        /// <summary>
        /// Create a commit for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#create-a-commit
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commit">The commit to create</param>
        Task<Commit> Create(string owner, string name, NewCommit commit);

        /// <summary>
        /// Create a commit for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#create-a-commit
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commit">The commit to create</param>
        Task<Commit> Create(long repositoryId, NewCommit commit);
    }
}