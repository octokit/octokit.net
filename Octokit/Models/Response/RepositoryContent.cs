using System;
using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents a piece of content in the repository. This could be a submodule, a symlink, a directory, or a file.
    /// Look at the Type property to figure out which one it is.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryContent : RepositoryContentInfo
    {
        public RepositoryContent() { }

        public RepositoryContent(string name, string path, string sha, int size, ContentType type, Uri downloadUrl, Uri url, Uri gitUrl, Uri htmlUrl, string encoding, string encodedContent, string target, Uri submoduleGitUrl)
            : base(name, path, sha, size, type, downloadUrl, url, gitUrl, htmlUrl)
        {
            Encoding = encoding;
            EncodedContent = encodedContent;
            Target = target;
            SubmoduleGitUrl = submoduleGitUrl;
        }

        /// <summary>
        /// The encoding of the content if this is a file. Typically "base64". Otherwise it's null.
        /// </summary>
        public string Encoding { get; protected set; }

        /// <summary>
        /// The Base64 encoded content if this is a file. Otherwise it's null.
        /// </summary>
        [Parameter(Key = "content")]
        public string EncodedContent { get; private set; }

        /// <summary>
        /// The unencoded content. Only access this if the content is expected to be text and not binary content.
        /// </summary>
        public string Content
        {
            get
            {
                return EncodedContent != null
                    ? EncodedContent.FromBase64String()
                    : null;
            }
        }

        /// <summary>
        /// Path to the target file in the repository if this is a symlink. Otherwise it's null.
        /// </summary>
        public string Target { get; protected set; }

        /// <summary>
        /// The location of the submodule repository if this is a submodule. Otherwise it's null.
        /// </summary>
        public Uri SubmoduleGitUrl { get; protected set; }
    }
}
