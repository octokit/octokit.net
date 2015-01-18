using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitTag : GitReference
    {
        public string Tag { get; protected set; }

        public string Message { get; protected set; }

        public SignatureResponse Tagger { get; protected set; }

        public TagObject Object { get; protected set; }
    }
}
