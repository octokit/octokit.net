using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Hook
    {
        /// <summary>
        /// The hook Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The URL for this repository hook.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The test URL for this repository hook.
        /// </summary>
        public string TestUrl { get; set; }

        /// <summary>
        /// The name of this hook.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A flag indicating if this hook is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The events subscribed by this hook.
        /// </summary>
        public IReadOnlyCollection<string> Events { get; set; }

        /// <summary>
        /// The configuration for this hook.
        /// </summary>
        public IReadOnlyDictionary<string, object> Config { get; set; }

        /// <summary>
        /// The latest response.
        /// </summary>
        public IReadOnlyDictionary<string, object> LastResponse { get; set; }

        /// <summary>
        /// The date the repository comment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date the repository comment was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Id: {0}, CreatedAt: {1}", Id, CreatedAt);
            }
        }
    }
}