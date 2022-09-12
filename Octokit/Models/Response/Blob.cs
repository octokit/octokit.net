using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Blob
    {
        public Blob() { }

        public Blob(string nodeId, string content, EncodingType encoding, string sha, int size)
        {
            NodeId = nodeId;
            Content = content;
            Encoding = encoding;
            Sha = sha;
            Size = size;
        }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The content of the blob.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// The encoding of the blob.
        /// </summary>
        public StringEnum<EncodingType> Encoding { get; private set; }

        /// <summary>
        /// The SHA of the blob.
        /// </summary>
        public string Sha { get; private set; }

        /// <summary>
        /// The size of the blob.
        /// </summary>
        public int Size { get; private set; }

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
        [Parameter(Value = "utf-8")]
        Utf8,

        [Parameter(Value = "base64")]
        Base64
    }
}
