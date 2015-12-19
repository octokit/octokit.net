using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create a Blob.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewBlob
    {
        /// <summary>
        /// The content of the blob.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The encoding of the blob.
        /// </summary>
        public EncodingType Encoding { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Encoding: {0}", Encoding);
            }
        }
    }
}