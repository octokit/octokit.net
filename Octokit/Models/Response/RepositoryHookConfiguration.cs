using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryHookConfiguration
    {
        public string Url { get; set; }
        public string ContentType { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Send {0} to {1}", ContentType, Url);
            }
        }
    }
}