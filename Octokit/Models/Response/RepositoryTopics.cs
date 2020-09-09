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
        public RepositoryTopics() { }

        public RepositoryTopics(IEnumerable<string> names)
        {
            Ensure.ArgumentNotNull(names, "names");
            Names = new ReadOnlyCollection<string>(names.ToList());
        }

        public IReadOnlyList<string> Names { get; protected set; }

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
