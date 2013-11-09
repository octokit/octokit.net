using System;

namespace Octokit
{
    /// <summary>
    /// search user response
    /// </summary>
    public class SearchUser
    {
        /// <summary>
        /// repo name
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// users avatar url
        /// </summary>
        public string AvatarUrl { get; set; }
    }

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
        /// Repo where this file belongs to
        /// </summary>
        public Repository Repository { get; set; }
    }

}