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
            CommitComments = new CommitCommentReactionClient(apiConnection);
        }

        public ICommitCommentReactionClient CommitComments { get; private set; }
    }
}
