namespace Octokit
{
    public class ReactionsClient : ApiClient, IReactionsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Reactions API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ReactionsClient(IApiConnection apiConnection) 
            : base(apiConnection)
        {
            CommitComments = new CommitCommentReaction(apiConnection);
        }

        public ICommitCommentReaction CommitComments { get; private set; }
    }
}
