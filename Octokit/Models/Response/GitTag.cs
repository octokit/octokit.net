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


        public string Tag { get; protected set; }

        public string Message { get; protected set; }

        public Committer Tagger { get; protected set; }

        public TagObject Object { get; protected set; }

        public Verification Verification { get; protected set; }
    }
}
