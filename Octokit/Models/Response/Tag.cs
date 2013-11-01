namespace Octokit.Response
{
    public class Tag
    {
        public string Name { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public Tagger Tagger { get; set; }
        public TagObject Object { get; set; }
    }
}