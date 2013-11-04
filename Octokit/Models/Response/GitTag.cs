namespace Octokit
{
    public class GitTag
    {
        public string Tag { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public UserAction Tagger { get; set; }
        public TagObject Object { get; set; }
    }
}