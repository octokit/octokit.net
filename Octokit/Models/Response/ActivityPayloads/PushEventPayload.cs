using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PushEventPayload : ActivityPayload
    {
        public long PushId { get; private set; }
        public int DistinctSize { get; private set; }
        public string Before { get; private set; }
        public string Head { get; private set; }
        public string Ref { get; private set; }
        public int Size { get; private set; }
        public IReadOnlyList<Commit> Commits { get; private set; }
    }
}
