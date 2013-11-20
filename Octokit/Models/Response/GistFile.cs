using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class GistFile
    {
        /// <summary>
        /// The size in bytes of the file.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The name of the file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public string Filename { get; set; }

        /// <summary>
        /// The mime type of the file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; set; }

        /// <summary>
        /// The programming language of the file, if any.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The text content of the file.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The url to download the file.
        /// </summary>
        public string RawUrl { get; set; }
    }
}