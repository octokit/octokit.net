using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Reference
    {
        public string Ref { get; set; }
        public string Url { get; set; }
        public TagObject Object { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Ref: {0}", Ref);
            }
        }
    }
}