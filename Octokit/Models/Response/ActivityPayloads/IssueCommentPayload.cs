namespace Octokit.Models.Response.ActivityPayloads
{
    public class IssueCommentPayload : ActivityPayload
    {
        // should always be "created" according to github api docs
        public string Action { get; set; }
        public Issue Issue { get; set; }
        public IssueComment Comment { get; set; }

    }
}
