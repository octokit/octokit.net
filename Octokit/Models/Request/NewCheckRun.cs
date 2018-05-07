namespace Octokit
{
    public class NewCheckRun : CheckRunUpdate
    {
        public string HeadBranch { get; set; }
        public string HeadSha { get; set; }
    }
}
