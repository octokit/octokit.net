namespace Octokit.Models.Request
{
    public sealed class NewCheckRun : CheckRunUpdate
    {
        public string HeadBranch { get; set; }
        public string HeadSha { get; set; }
    }
}
