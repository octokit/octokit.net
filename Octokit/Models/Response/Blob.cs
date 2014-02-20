using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Blob
    {
        /// <summary>
        /// The content of the blob.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The encoding of the blob.
        /// </summary>
        public EncodingType Encoding { get; set; }

        /// <summary>
        /// The SHA of the blob.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// The size of the blob.
        /// </summary>
        public int Size { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Sha: {0} Size: {1}", Sha, Size);
            }
        }
    }

    public enum EncodingType
    {
        Utf8,
        Base64
    }
}