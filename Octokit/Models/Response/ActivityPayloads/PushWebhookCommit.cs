using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PushWebhookCommit
    {
        public string Id { get; protected set; }

        public string TreeId { get; protected set; }

        public bool Distinct { get; protected set; }

        public string Message { get; protected set; }

        public DateTimeOffset Timestamp { get; protected set; }

        public Uri Url { get; protected set; }

        public Committer Author { get; protected set; }

        public Committer Committer { get; protected set; }

        public IReadOnlyList<string> Added { get; protected set; }

        public IReadOnlyList<string> Removed { get; protected set; }

        public IReadOnlyList<string> Modified { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0}", Id);
            }
        }
    }
}
