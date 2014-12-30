using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitTag : GitReference
    {
        public string Tag { get; set; }
        public string Message { get; set; }
        public SignatureResponse Tagger { get; set; }
        public TagObject Object { get; set; }
    }
}