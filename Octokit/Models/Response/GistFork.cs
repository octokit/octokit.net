using System;

namespace Octokit
{
    public class GistFork
    {
        /// <summary>
        /// The <see cref="User"/> that created this <see cref="GistFork"/>
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The API URL for this <see cref="GistFork"/>.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
    }
}