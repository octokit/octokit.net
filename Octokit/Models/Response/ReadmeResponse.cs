namespace Octokit
{
    internal class ReadmeResponse
    {
        public ReadmeResponse() { }

        public ReadmeResponse(string content, string name, string htmlUrl, string url, string encoding)
        {
            Content = content;
            Name = name;
            HtmlUrl = htmlUrl;
            Url = url;
            Encoding = encoding;
        }

        public string Content { get; private set; }
        public string Name { get; private set; }
        public string HtmlUrl { get; private set; }
        public string Url { get; private set; }
        public string Encoding { get; private set; }
    }
}
