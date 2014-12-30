using System;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents a piece of content in the repository. This could be a submodule, a symlink, a directory, or a file.
    /// Look at the Type property to figure out which one it is.
    /// </summary>
    public class RepositoryContent : RepositoryContentInfo
    {
        /// <summary>
        /// The encoding of the content if this is a file. Typically "base64". Otherwise it's null.
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// The encoded content if this is a file. Otherwise it's null.
        /// </summary>
        [Parameter(Key = "content")]
        protected string EncodedContent { get; set; }

        public string Content
        {
            get { return EncodedContent.FromBase64String(); }
        }

        /// <summary>
        /// Path to the target file in the repository if this is a symlink. Otherwise it's null.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// The location of the submodule repository if this is a submodule. Otherwise it's null.
        /// </summary>
        public Uri SubmoduleGitUrl { get; set; }
    }
}