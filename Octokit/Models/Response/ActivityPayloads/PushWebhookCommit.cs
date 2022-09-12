using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PushWebhookCommit
    {
        public string Id { get; private set; }

        public string TreeId { get; private set; }

        public bool Distinct { get; private set; }

        public string Message { get; private set; }

        public DateTimeOffset Timestamp { get; private set; }

        public Uri Url { get; private set; }

        public Committer Author { get; private set; }

        public Committer Committer { get; private set; }

        public IReadOnlyList<string> Added { get; private set; }

        public IReadOnlyList<string> Removed { get; private set; }

        public IReadOnlyList<string> Modified { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0}", Id);
            }
        }
    }
}
