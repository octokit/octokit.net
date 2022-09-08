using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TagObject : GitReference
    {
        public TagObject() { }

        public TagObject(string nodeId, string url, string label, string @ref, string sha, User user, Repository repository, TaggedType type)
            : base(nodeId, url, label, @ref, sha, user, repository)
        {
            Type = type;
        }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "Name defined by web api and required for deserialization")]
        public StringEnum<TaggedType> Type { get; private set; }
    }

    /// <summary>
    /// Represents the type of object being tagged
    /// </summary>
    public enum TaggedType
    {
        [Parameter(Value = "commit")]
        Commit,

        [Parameter(Value = "blob")]
        Blob,

        [Parameter(Value = "tree")]
        Tree,

        [Parameter(Value = "tag")]
        Tag
    }
}
