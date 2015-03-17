namespace Octokit
{
    public class GitIgnoreTemplate
    {
        public GitIgnoreTemplate(string name, string source)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(source, "source");

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
