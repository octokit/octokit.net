using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReferenceUpdate
    {
        public ReferenceUpdate(string sha) : this(sha, false)
        {
        }

        public ReferenceUpdate(string sha, bool force)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            Sha = sha;
            Force = force;
        }

        public string Sha { get; private set; }
        public bool Force { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Sha: {0} Force: {1}", Sha, Force);
            }
        }
    }
}