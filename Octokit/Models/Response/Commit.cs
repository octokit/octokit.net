namespace Octokit
{
    public class Commit
    {
        public string Sha { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public UserAction Author { get; set; }
        public UserAction Commiter { get; set; }
        public CommitRelation Tree { get; set; }
        public CommitRelation[] Parents { get; set; }
    }

    public class CommitRelation
    {
        public string Url { get; set; }
        public string Sha { get; set; }
    }

}