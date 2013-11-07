namespace Octokit
{
    public class GitTag : GitReference
    {
        public string Tag { get; set; }
        public string Message { get; set; }
        public UserAction Tagger { get; set; }
        public TagObject Object { get; set; }
    }
}