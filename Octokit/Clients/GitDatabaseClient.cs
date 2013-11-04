namespace Octokit
{
    public class GitDatabaseClient : ApiClient, IGitDatabaseClient
    {
        public GitDatabaseClient(IApiConnection apiConnection) 
            : base(apiConnection)
        {
            this.Tag = new TagsClient(apiConnection);
        }

        public ITagsClient Tag { get; set; }
    }
}