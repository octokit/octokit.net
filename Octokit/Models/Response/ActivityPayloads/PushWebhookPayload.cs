using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PushWebhookPayload : ActivityPayload
    {
        public string Head { get; protected set; }
        public string Before { get; protected set; }
        public string After { get; protected set; }
        public string Ref { get; protected set; }
        public string BaseRef { get; protected set; }
        public bool Created { get; protected set; }
        public bool Deleted { get; protected set; }
        public bool Forced { get; protected set; }
        public string Compare { get; protected set; }
        public int Size { get; protected set; }
        public IReadOnlyList<PushWebhookCommit> Commits { get; protected set; }
        public PushWebhookCommit HeadCommit { get; protected set; }
    }
}
