using Octokit.Internal;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TagObject : GitReference
    {
        public TagObject() { }

        public TagObject(string url, string label, string @ref, string sha, User user, Repository repository)
            : base(url, label, @ref, sha, user, repository)
        {
        }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "Name defined by web api and required for deserialization")]
        [Parameter(Key = "IgnoreThisField")]
        public TaggedType? Type { get { return TypeText.ParseEnumWithDefault(TaggedType.Unknown); } }

        [Parameter(Key = "type")]
        public string TypeText { get; protected set; }
    }

    /// <summary>
    /// Represents the type of object being tagged
    /// </summary>
    public enum TaggedType
    {
        Commit,
        Blob,
        Tree,
        Tag,
        /// <summary>
        /// Used as a placeholder for unknown fields
        /// </summary>
        Unknown
    }
}