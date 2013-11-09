using System;

namespace Octokit
{
    public class SearchCode
    {
        /// <summary>
        /// file name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// path to file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Sha for file
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// api-url to file
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// git-url to file
        /// </summary>
        public Uri GitUrl { get; set; }

        /// <summary>
        /// html-url to file
        /// </summary>
        public Uri HtmlUrl { get; set; }

        /// <summary>
        /// Repo where this file belongs to
        /// </summary>
        public Repository Repository { get; set; }
    }

}