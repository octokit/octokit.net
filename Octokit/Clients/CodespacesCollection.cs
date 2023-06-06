using Octokit.Internal;
using System.Collections.Generic;

namespace Octokit
{
    public class CodespacesCollection
    {
        public CodespacesCollection() { }
        [Parameter(Key = "total_count")]
        public int Count { get; private set; }
        [Parameter(Key = "codespaces")]
        public IReadOnlyList<Codespace> Codespaces { get; private set; }
    }
}