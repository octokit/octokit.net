using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitIgnoreTemplate
    {
        public GitIgnoreTemplate(string name, string source)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(source, "source");

            Name = name;
            Source = source;
        }

        public GitIgnoreTemplate()
        {
        }

        public string Name { get; protected set; }
        public string Source { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "GitIgnore: {0}", Name);
            }
        }
    }
}
