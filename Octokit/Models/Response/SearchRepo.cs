using System;

namespace Octokit
{
    /// <summary>
    /// Search repo response
    /// </summary>
    public class SearchRepo
    {
        /// <summary>
        /// repo name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// full name of repo e.g. dtrupenn/Tetris
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// owner of repo
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// is a private repo?
        /// </summary>
        public bool Private { get; set; }

        /// <summary>
        /// description of repo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// is repo a fork
        /// </summary>
        public bool Fork { get; set; }
    }

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

    public class SearchIssue
    {
        /// <summary>
        /// issue id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// number inside the repo
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// author of this issue
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// title of issue
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// comments on this issue
        /// </summary>
        public int Comments { get; set; }
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