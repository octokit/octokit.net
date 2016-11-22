using Octokit.Internal;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Blob
    {
        public Blob() { }

        public Blob(string content, string sha, int size)
        {
            Content = content;
            Sha = sha;
            Size = size;
        }

        /// <summary>
        /// The content of the blob.
        /// </summary>
        public string Content { get; protected set; }

        /// <summary>
        /// The encoding of the blob.
        /// </summary>
        [Parameter(Key = "IgnoreThisField")]
        public EncodingType? Encoding { get { return EncodingText.ParseEnumWithDefault<EncodingType>(EncodingType.Unknown); } }

        [Parameter(Key = "encoding")]
        public string EncodingText { get; protected set; }

        /// <summary>
        /// The SHA of the blob.
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// The size of the blob.
        /// </summary>
        public int Size { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0} Size: {1}", Sha, Size);
            }
        }
    }

    public enum EncodingType
    {
        Utf8,
        Base64,
        /// <summary>
        /// Used as a placeholder for unknown fields
        /// </summary>
        Unknown
    }
}