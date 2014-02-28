using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistUpdate
    {
        public GistUpdate()
        {
            Files = new Dictionary<string, GistFileUpdate>();
        }

        public string Description { get; set; }
        public IDictionary<string, GistFileUpdate> Files { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Description: {0}", Description);
            }
        }
    }

    public class GistFileUpdate
    {
        public string NewFileName { get; set; }
        public string Content { get; set; }
    }
}
