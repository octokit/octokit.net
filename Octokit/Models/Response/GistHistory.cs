using System;

namespace Octokit
{
    /// <summary>
    /// A historical version of a <see cref="Gist"/>
    /// </summary>
    public class GistHistory
    {
        /// <summary>
        /// The url that can be used by the API to retrieve this version of the <see cref="Gist"/>.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A git sha representing the version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The <see cref="User"/> who create this version.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// A <see cref="GistHistory"/> that represents the level of change for this <see cref="GistHistory"/>.
        /// </summary>
        public GistChangeStatus ChangeStatus { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> the version was created.
        /// </summary>
        public DateTimeOffset CommittedAt { get; set; }
    }
}