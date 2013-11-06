namespace Octokit
{
    public class Blob
    {
        /// <summary>
        /// The content of the blob.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The encoding of the blob.
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// The SHA of the blob.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// The size of the blob.
        /// </summary>
        public int Size { get; set; }
    }
}