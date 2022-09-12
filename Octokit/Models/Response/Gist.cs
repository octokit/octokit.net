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

        public Gist(string url, string id, string nodeId, string description, bool @public, User owner, IReadOnlyDictionary<string, GistFile> files, int comments, string commentsUrl, string htmlUrl, string gitPullUrl, string gitPushUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt, IReadOnlyList<GistFork> forks, IReadOnlyList<GistHistory> history)
        {
            Url = url;
            Id = id;
            NodeId = nodeId;
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
        public string Url { get; private set; }

        /// <summary>
        /// The Id of this <see cref="Gist"/>.
        /// </summary>
        /// <remarks>
        /// Given a gist url of https://gist.github.com/UserName/1234 the Id would be '1234'.
        /// </remarks>
        public string Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// A description of the <see cref="Gist"/>.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Indicates if the <see cref="Gist"/> is private or public.
        /// </summary>
        public bool Public { get; private set; }

        /// <summary>
        /// The <see cref="User"/> who owns this <see cref="Gist"/>.
        /// </summary>
        /// <remarks>
        /// Given a gist url of https://gist.github.com/UserName/1234 the Owner would be 'UserName'.
        /// </remarks>
        public User Owner { get; private set; }

        /// <summary>
        /// A <see cref="IDictionary{TKey,TValue}"/> containing all <see cref="GistFile"/>s in this <see cref="Gist"/>.
        /// </summary>
        public IReadOnlyDictionary<string, GistFile> Files { get; private set; }

        /// <summary>
        /// The number of comments on this <see cref="Gist"/>.
        /// </summary>
        public int Comments { get; private set; }

        /// <summary>
        /// A url to retrieve the comments for this <see cref="Gist"/>.
        /// </summary>
        public string CommentsUrl { get; private set; }

        /// <summary>
        /// URL to view the gist on gist.github.com.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// The git url to pull from to retrieve the contents for this <see cref="Gist"/>.
        /// </summary>
        public string GitPullUrl { get; private set; }

        /// <summary>
        /// The git url to push to when changing this <see cref="Gist"/>.
        /// </summary>
        public string GitPushUrl { get; private set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        /// <summary>
        /// A <see cref="IList{T}"/> of all <see cref="GistFork"/> that exist for this <see cref="Gist"/>.
        /// </summary>
        public IReadOnlyList<GistFork> Forks { get; private set; }

        /// <summary>
        /// A <see cref="IList{T}"/> of all <see cref="GistHistory"/> containing the full history for this <see cref="Gist"/>.
        /// </summary>
        public IReadOnlyList<GistHistory> History { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Description: {0}", Description);
            }
        }
    }
}
