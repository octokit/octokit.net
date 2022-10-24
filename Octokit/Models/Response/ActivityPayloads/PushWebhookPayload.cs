using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PushWebhookPayload : ActivityPayload
    {
        public string Head { get; private set; }
        public string Before { get; private set; }
        public string After { get; private set; }
        public string Ref { get; private set; }
        public string BaseRef { get; private set; }
        public bool Created { get; private set; }
        public bool Deleted { get; private set; }
        public bool Forced { get; private set; }
        public string Compare { get; private set; }
        public int Size { get; private set; }
        public IReadOnlyList<PushWebhookCommit> Commits { get; private set; }
        public PushWebhookCommit HeadCommit { get; private set; }
    }
}
