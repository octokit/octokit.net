namespace Octokit
{
    public class GitDatabaseClient : ApiClient, IGitDatabaseClient
    {
        public GitDatabaseClient(IApiConnection apiConnection) 
            : base(apiConnection)
        {
            Tag = new TagsClient(apiConnection);
        }

        public ITagsClient Tag { get; set; }
    }
}