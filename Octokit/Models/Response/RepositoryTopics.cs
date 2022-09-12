using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTopics
    {
        public RepositoryTopics() { Names = new List<string>(); }

        public RepositoryTopics(IEnumerable<string> names)
        {
            var initialItems = names?.ToList() ?? new List<string>();
            Names = new ReadOnlyCollection<string>(initialItems);
        }

        public IReadOnlyList<string> Names { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "RepositoryTopics: Names: {0}", string.Join(", ", Names));
            }
        }
    }
}
