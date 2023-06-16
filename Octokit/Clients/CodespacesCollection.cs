using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CodespacesCollection
    {
        public CodespacesCollection(IReadOnlyList<Codespace> codespaces, int count)
        {
            Codespaces = codespaces;
            Count = count;
        }

        public CodespacesCollection() { }

        [Parameter(Key = "total_count")]
        public int Count { get; private set; }
        [Parameter(Key = "codespaces")]
        public IReadOnlyList<Codespace> Codespaces { get; private set; } = new List<Codespace>();

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "CodespacesCollection: Count: {0}", Count);
    }
}