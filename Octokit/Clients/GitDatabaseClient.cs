namespace Octokit
{
    public class GitDatabaseClient : ApiClient, IGitDatabaseClient
    {
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