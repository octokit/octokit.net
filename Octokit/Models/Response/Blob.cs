using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Blob
    {
        public Blob() { }

        public Blob(string content, EncodingType encoding, string sha, int size)
        {
            Content = content;
            Encoding = encoding;
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
        public EncodingType Encoding { get; protected set; }

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
        Base64
    }
}