using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// An entry in the activity event stream
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Activity
    {
        public Activity() { }

        public Activity(string type, bool @public, Repository repo, User actor, Organization org, DateTimeOffset createdAt, string id, ActivityPayload payload)
        {
            Type = type;
            Public = @public;
            Repo = repo;
            Actor = actor;
            Org = org;
            CreatedAt = createdAt;
            Id = id;
            Payload = payload;
        }

        /// <summary>
        /// The type of the activity.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; private set; }

        /// <summary>
        /// Whether the activity event is public or not.
        /// </summary>
        public bool Public { get; private set; }

        /// <summary>
        /// The repository associated with the activity event.
        /// </summary>
        public Repository Repo { get; private set; }

        /// <summary>
        /// The user associated with the activity event.
        /// </summary>
        public User Actor { get; private set; }

        /// <summary>
        /// The organization associated with the activity event.
        /// </summary>
        public Organization Org { get; private set; }

        /// <summary>
        /// The date the activity event was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The activity event Id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The payload associated with the activity event.
        /// </summary>
        public ActivityPayload Payload { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Type: {0}", Type);
            }
        }
    }
}
