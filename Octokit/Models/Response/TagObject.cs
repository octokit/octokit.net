using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TagObject : GitReference
    {
        public TagObject() { }

        public TagObject(string url, string label, string @ref, string sha, User user, Repository repository, TaggedType type)
            : base(url, label, @ref, sha, user, repository)
        {
            Type = type;
        }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "Name defined by web api and required for deserialisation")]
        public TaggedType Type { get; protected set; }
    }

    /// <summary>
    /// Represents the type of object being tagged
    /// </summary>
    public enum TaggedType
    {
        Commit,
        Blob,
        Tree,
        Tag
    }
}