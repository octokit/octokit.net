namespace Octokit.Models.Response.ActivityPayloads
{
    public class CommitCommentPayload : ActivityPayload
    {
        public CommitComment Comment { get; set; }

        public User Sender { get; set; }
    }
}
