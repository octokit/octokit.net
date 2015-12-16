using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Gist
    {
        public Gist() { }

        public Gist(string url, string id, string description, bool @public, User owner, IReadOnlyDictionary<string, GistFile> files, int comments, string commentsUrl, string htmlUrl, string gitPullUrl, string gitPushUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt, IReadOnlyList<GistFork> forks, IReadOnlyList<GistHistory> history)
        {
            Url = url;
            Id = id;
            Description = description;
            Public = @public;
            Owner = owner;
            Files = files;
            Comments = comments;
            CommentsUrl = commentsUrl;
            HtmlUrl = htmlUrl;
            GitPullUrl = gitPullUrl;
            GitPushUrl = gitPushUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Forks = forks;
            History = history;
        }

        /// <summary>
        /// The API URL for this <see cref="Gist"/>.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The Id of this <see cref="Gist"/>.
        /// </summary>
        /// <remarks>
        /// Given a gist url of https://gist.github.com/UserName/1234 the Id would be '1234'.
        /// </remarks>
        public string Id { get; protected set; }

        /// <summary>
        /// A description of the <see cref="Gist"/>.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Indicates if the <see cref="Gist"/> is private or public.
        /// </summary>
        public bool Public { get; protected set; }

        /// <summary>
        /// The <see cref="User"/> who owns this <see cref="Gist"/>.
        /// </summary>
        /// <remarks>
        /// Given a gist url of https://gist.github.com/UserName/1234 the Owner would be 'UserName'.
        /// </remarks>
        public User Owner { get; protected set; }

        /// <summary>
        /// A <see cref="IDictionary{TKey,TValue}"/> containing all <see cref="GistFile"/>s in this <see cref="Gist"/>.
        /// </summary>
        public IReadOnlyDictionary<string, GistFile> Files { get; protected set; }

        /// <summary>
        /// The number of comments on this <see cref="Gist"/>.
        /// </summary>
        public int Comments { get; protected set; }

        /// <summary>
        /// A url to retrieve the comments for this <see cref="Gist"/>.
        /// </summary>
        public string CommentsUrl { get; protected set; }

        /// <summary>
        /// URL to view the gist on gist.github.com.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The git url to pull from to retrieve the contents for this <see cref="Gist"/>.
        /// </summary>
        public string GitPullUrl { get; protected set; }

        /// <summary>
        /// The git url to push to when changing this <see cref="Gist"/>.
        /// </summary>
        public string GitPushUrl { get; protected set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        /// <summary>
        /// A <see cref="IList{T}"/> of all <see cref="GistFork"/> that exist for this <see cref="Gist"/>.
        /// </summary>
        public IReadOnlyList<GistFork> Forks { get; protected set; }

        /// <summary>
        /// A <see cref="IList{T}"/> of all <see cref="GistHistory"/> containing the full history for this <see cref="Gist"/>.
        /// </summary>
        public IReadOnlyList<GistHistory> History { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Description: {0}", Description);
            }
        }
    }
}