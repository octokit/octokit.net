using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EditRepositoryHook
    {
        public dynamic Config { get; set; }
        public IEnumerable<string> Events { get; set; }
        public IEnumerable<string> AddEvents { get; set; }
        public IEnumerable<string> RemoveEvents { get; set; }
        public bool Active { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Replacing Events: {0}, Adding Events: {1}, Removing Events: {2}", Events == null ? "no" : string.Join(", ", Events), 
                    AddEvents == null ? "no" : string.Join(", ", AddEvents), 
                    RemoveEvents == null ? "no" : string.Join(", ", RemoveEvents));
            }
        }
    }
}