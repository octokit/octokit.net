using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Octokit
{
    /// <summary>
    /// Used to upload a release asset.
    /// </summary>
    /// <remarks>
    /// This endpoint makes use of a Hypermedia relation to determine which URL to access. This endpoint is provided
    /// by a URI template in the release’s API response. You need to use an HTTP client which supports SNI to make
    /// calls to this endpoint. The asset data is expected in its raw binary form, rather than JSON. Everything else
    ///  about the endpoint is the same as the rest of the API. For example, you’ll still need to pass your
    ///  authentication to be able to upload an asset.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseAssetUpload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseAssetUpload"/> class.
        /// </summary>
        public ReleaseAssetUpload() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseAssetUpload"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">
        /// The content type of the asset. Example: "application/zip". For a list of acceptable types, refer this list 
        /// of <see href="https://en.wikipedia.org/wiki/Media_type#List_of_common_media_types">common media types</see>.
        /// </param>
        /// <param name="rawData">The raw data.</param>
        /// <param name="timeout">The timeout.</param>
        public ReleaseAssetUpload(string fileName, string contentType, Stream rawData, TimeSpan? timeout)
        {
            FileName = fileName;
            ContentType = contentType;
            RawData = rawData;
            Timeout = timeout;
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the raw data.
        /// </summary>
        /// <value>
        /// The raw data.
        /// </value>
        public Stream RawData { get; set; }

        /// <summary>
        /// Gets or sets the timeout.
        /// </summary>
        /// <value>
        /// The timeout.
        /// </value>
        public TimeSpan? Timeout { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "FileName: {0} ", FileName);
            }
        }
    }
}