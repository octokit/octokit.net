using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Author
    {
        public string Login { get; protected set; }

        public int Id { get; protected set; }

        public string AvatarUrl { get; protected set; }

        /// <summary>
        /// Hex Gravatar identifier, now obsolete
        /// </summary>
        /// <remarks>
        /// For more details: https://developer.github.com/changes/2014-09-05-removing-gravatar-id/
        /// </remarks>
        [Obsolete("This property is now obsolete, use AvatarUrl instead")]
        public string GravatarId { get; protected set; }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string FollowersUrl { get; protected set; }

        public string FollowingUrl { get; protected set; }

        public string GistsUrl { get; protected set; }

        public string StarredUrl { get; protected set; }

        public string SubscriptionsUrl { get; protected set; }

        public string OrganizationsUrl { get; protected set; }

        public string ReposUrl { get; protected set; }

        public string EventsUrl { get; protected set; }

        public string ReceivedEventsUrl { get; protected set; }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This is what is returned from the api")]
        public string Type { get; protected set; }

        public bool SiteAdmin { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Author: Id: {0} Login: {1}",Id, Login);
            }
        }
    }
}