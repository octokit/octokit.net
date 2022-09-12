using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCode
    {
        public SearchCode() { }

        public SearchCode(string name, string path, string sha, string url, string gitUrl, string htmlUrl, Repository repository)
        {
            Name = name;
            Path = path;
            Sha = sha;
            Url = url;
            GitUrl = gitUrl;
            HtmlUrl = htmlUrl;
            Repository = repository;
        }

        /// <summary>
        /// file name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// path to file
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Sha for file
        /// </summary>
        public string Sha { get; private set; }

        /// <summary>
        /// api-url to file
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// git-url to file
        /// </summary>
        public string GitUrl { get; private set; }

        /// <summary>
        /// html-url to file
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// Repo where this file belongs to
        /// </summary>
        public Repository Repository { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Sha: {0} Name: {1}", Sha, Name); }
        }
    }
}
