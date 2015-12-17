using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create a new tag
    /// </summary>
    /// <remarks>
    /// Note that creating a tag object does not create the reference that makes a tag in Git. If you want to create
    ///  an annotated tag in Git, you have to do this call to create the tag object, and then create the
    ///  refs/tags/[tag] reference. If you want to create a lightweight tag, you only have to create the tag reference
    ///  - this call would be unnecessary.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewTag
    {
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the tag message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// The SHA of the git object this is tagging
        /// </summary>
        /// <value>
        /// The object.
        /// </value>
        public string Object { get; set; }

        /// <summary>
        /// The type of the object we’re tagging. Normally this is a commit but it can also be a tree or a blob.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Property name as defined by web api")]
        public TaggedType Type { get; set; }

        /// <summary>
        /// An object with information about the individual creating the tag.
        /// </summary>
        /// <value>
        /// The tagger.
        /// </value>
        public Committer Tagger { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Tag {0} Type: {1}", Tag, Type);
            }
        }
    }
}