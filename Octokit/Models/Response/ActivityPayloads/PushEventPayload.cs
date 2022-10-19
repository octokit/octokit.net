using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PushEventPayload : ActivityPayload
    {
        public string Head { get; private set; }
        public string Ref { get; private set; }
        public int Size { get; private set; }
        public IReadOnlyList<Commit> Commits { get; private set; }
    }
}
