using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TagObject : GitReference
    {
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