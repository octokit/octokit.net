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

        public string Content { get; protected set; }
        public string Name { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public string Url { get; protected set; }
        public string Encoding { get; protected set; }
    }
}