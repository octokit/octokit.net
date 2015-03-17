namespace Octokit
{
    public class GitIgnoreTemplate
    {
        public GitIgnoreTemplate(string name, string source)
        {
            Name = name;
            Source = source;
        }

        public GitIgnoreTemplate()
        {
        }

        public string Name { get; protected set; }
        public string Source { get; protected set; }
    }
}
