using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitIgnoreTemplate
    {
        public GitIgnoreTemplate(string name, string source)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(source, nameof(source));

            Name = name;
            Source = source;
        }

        public GitIgnoreTemplate()
        {
        }

        public string Name { get; private set; }
        public string Source { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "GitIgnore: {0}", Name);
            }
        }
    }
}
