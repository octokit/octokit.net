using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PushEventPayload : ActivityPayload
    {
        public string Head { get; protected set; }
        public string Ref { get; protected set; }
        public int Size { get; protected set; }
        public IReadOnlyList<Commit> Commits { get; protected set; }
    }
}
