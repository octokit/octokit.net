namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Database API. Gives you access to read and write raw Git objects and to list and update your references.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/">Git API documentation</a> for more information.
    /// </remarks>
    public class GitDatabaseClient : ApiClient, IGitDatabaseClient
    {
        /// <summary>
        /// Instantiates a new GitHub Git API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public GitDatabaseClient(IApiConnection apiConnection) 
            : base(apiConnection)
        {
            Blob = new BlobsClient(apiConnection);
            Tree = new TreesClient(apiConnection);
            Tag = new TagsClient(apiConnection);
            Commit = new CommitsClient(apiConnection);
            Reference = new ReferencesClient(apiConnection);
        }

        public IBlobsClient Blob { get; private set; }
        public ITreesClient Tree { get; private set; }
        public ITagsClient Tag { get; private set; }
        public ICommitsClient Commit { get; private set; }
        public IReferencesClient Reference { get; private set; }
    }
}