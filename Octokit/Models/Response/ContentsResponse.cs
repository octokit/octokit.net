using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class ContentsResponse
    {
        /// <summary>
        /// The type of item be it a directory or file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Matches the property name used by the API")]
        public ResponseType Type { get; set; }

        /// <summary>
        /// The size of the file
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The name of the directory or file
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The path to the directory or file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// This property only is returned from the API if it's a file
        /// It's a base64 encoded string of the contents of the file
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The Sha of the directory or file
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// Url of request
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The url to the specific blob Url in Git
        /// </summary>
        public Uri GitUrl { get; set; }

        /// <summary>
        /// The Html Url to the directory or file
        /// </summary>
        public Uri HtmlUrl { get; set; }
    }

    /// <summary>
    /// Represents the Response Type for the Content API
    /// </summary>
    public enum ResponseType
    {
        /// <summary>
        /// This item is a file
        /// </summary>
        File,

        /// <summary>
        /// This item is a directory
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dir", Justification = "Matches the value returned by the API")]
        Dir
    }
}
