using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistRequest : RequestParameters
    {
        public GistRequest(DateTimeOffset since)
        {
            Since = since;
        }

        public DateTimeOffset Since { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Since: {0}", Since);
            }
        }
    }
}
