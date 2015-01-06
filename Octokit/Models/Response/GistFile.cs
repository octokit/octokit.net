using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class GistFile
    {
        /// <summary>
        /// The size in bytes of the file.
        /// </summary>
        public int Size { get; protected set; }

        /// <summary>
        /// The name of the file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public string Filename { get; protected set; }

        /// <summary>
        /// The mime type of the file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; protected set; }

        /// <summary>
        /// The programming language of the file, if any.
        /// </summary>
        public string Language { get; protected set; }

        /// <summary>
        /// The text content of the file.
        /// </summary>
        public string Content { get; protected set; }

        /// <summary>
        /// The url to download the file.
        /// </summary>
        public string RawUrl { get; protected set; }
    }
}