using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryHookConfiguration
    {
        public RepositoryHookConfiguration(string contentType, string url)
        {
            ContentType = contentType;
            Url = url;
        }

        public string Url { get; private set; }

        public string ContentType { get; private set; }

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