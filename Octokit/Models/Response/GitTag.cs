using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitTag : GitReference
    {
        public GitTag() { }

        public GitTag(string nodeId, string url, string label, string @ref, string sha, User user, Repository repository, string tag, string message, Committer tagger, TagObject @object, Verification verification)
            : base(nodeId, url, label, @ref, sha, user, repository)
        {
            Tag = tag;
            Message = message;
            Tagger = tagger;
            Object = @object;
            Verification = verification;
        }


        public string Tag { get; private set; }

        public string Message { get; private set; }

        public Committer Tagger { get; private set; }

        public TagObject Object { get; private set; }

        public Verification Verification { get; private set; }
    }
}
