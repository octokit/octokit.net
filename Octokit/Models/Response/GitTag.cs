using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitTag : GitReference
    {
        public GitTag() { }

        public GitTag(string url, string label, string @ref, string sha, User user, Repository repository, string tag, string message, Committer tagger, TagObject objectVar)
            : base(url, label, @ref, sha, user, repository)
        {
            Tag = tag;
            Message = message;
            Tagger = tagger;
            Object = objectVar;
        }

        public string Tag { get; protected set; }

        public string Message { get; protected set; }

        public Committer Tagger { get; protected set; }

        public TagObject Object { get; protected set; }
    }
}
