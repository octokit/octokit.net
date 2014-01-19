using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryHook
    {
        public string Url { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Events { get; set; }
        public bool Active { get; set; }
        public RepositoryHookConfiguration Config { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Name: {0} Url: {1}, Events: {2}", Name, Url, string.Join(", ", Events));
            }
        }
    }
}
