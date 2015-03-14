using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepositoryHook
    {
        public string Name { get; set; }
        public dynamic Config { get; set; }
        public IEnumerable<string> Events { get; set; }
        public bool Active { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Name: {0}, Events: {1}", Name,string.Join(", ", Events));
            }
        }
    }
}
