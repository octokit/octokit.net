namespace Octokit
{
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
    }
}