using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCode
    {
        public SearchCode() { }

        public SearchCode(string name, string path, string sha, Uri url, Uri gitUrl, Uri htmlUrl, Repository repository)
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
        public string Name { get; protected set; }

        /// <summary>
        /// path to file
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Sha for file
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// api-url to file
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// git-url to file
        /// </summary>
        public Uri GitUrl { get; protected set; }

        /// <summary>
        /// html-url to file
        /// </summary>
        public Uri HtmlUrl { get; protected set; }

        /// <summary>
        /// Repo where this file belongs to
        /// </summary>
        public Repository Repository { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Sha: {0} Name: {1}", Sha, Name); }
        }
    }
}
