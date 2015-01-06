using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistFileUpdate
    {
        public string NewFileName { get; set; }

        public string Content { get; set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "NewFileName: {0}", NewFileName); }
        }
    }
}