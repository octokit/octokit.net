using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Gist
    {
        /// <summary>
        /// The API URL for this <see cref="Gist"/>.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The Id of this <see cref="Gist"/>.
        /// </summary>
        /// <remarks>
        /// Given a gist url of https://gist.github.com/UserName/1234 the Id would be '1234'.
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// A description of the <see cref="Gist"/>.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates if the <see cref="Gist"/> is private or public.
        /// </summary>
        public bool Public { get; set; }

        /// <summary>
        /// The <see cref="User"/> who owns this <see cref="Gist"/>.
        /// </summary>
        /// <remarks>
        /// Given a gist url of https://gist.github.com/UserName/1234 the Owner would be 'UserName'.
        /// </remarks>
        public User Owner { get; set; }

        /// <summary>
        /// A <see cref="IDictionary{TKey,TValue}"/> containing all <see cref="GistFile"/>s in this <see cref="Gist"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IDictionary<string, GistFile> Files { get; set; }

        /// <summary>
        /// The number of comments on this <see cref="Gist"/>.
        /// </summary>
        public int Comments { get; set; }

        /// <summary>
        /// A url to retrieve the comments for this <see cref="Gist"/>.
        /// </summary>
        public string CommentsUrl { get; set; }

        public string HtmlUrl { get; set; }

        /// <summary>
        /// The git url to pull from to retrieve the contents for this <see cref="Gist"/>.
        /// </summary>
        public string GitPullUrl { get; set; }

        /// <summary>
        /// The git url to push to when changing this <see cref="Gist"/>.
        /// </summary>
        public string GitPushUrl { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// A <see cref="IList{T}"/> of all <see cref="GistFork"/> that exist for this <see cref="Gist"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<GistFork> Forks { get; set; }

        /// <summary>
        /// A <see cref="IList{T}"/> of all <see cref="GistHistory"/> containing the full history for this <see cref="Gist"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<GistHistory> History { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Description: {0}", Description);
            }
        }
    }
}