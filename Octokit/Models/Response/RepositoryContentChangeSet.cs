namespace Octokit
{
    /// <summary>
    /// The response from the Repository Contents API. The API assumes a dynamic client type so we need
    /// to model that.
    /// </summary>
    /// <remarks>https://developer.github.com/v3/repos/contents/</remarks>
    public class RepositoryContentChangeSet
    {
        /// <summary>
        /// The content of the response.
        /// </summary>
        public RepositoryContentInfo Content { get; set; }

        /// <summary>
        /// The commit information for the content change.
        /// </summary>
        public Commit Commit { get; set; }
    }
}