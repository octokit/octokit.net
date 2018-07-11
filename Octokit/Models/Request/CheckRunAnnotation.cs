using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunAnnotation
    {
        /// <summary>
        /// Constructs a CheckRunAnnotation request object
        /// </summary>
        /// <param name="filename">Required. The path of the file to add an annotation to. For example, assets/css/main.css.</param>
        /// <param name="blobHref">Required. The file's full blob URL. You can find the blob_href in the response of the Get a single commit endpoint, by reading the blob_url from an element of the files array. You can also construct the blob URL from the head_sha, the repository, and the filename: https://github.com/:owner/:repo/blob/:head_sha/:filename.</param>
        /// <param name="startLine">Required. The start line of the annotation.</param>
        /// <param name="endLine">Required. The end line of the annotation.</param>
        /// <param name="warningLevel">Required. The warning level of the annotation. Can be one of notice, warning, or failure.</param>
        /// <param name="message">Required. A short description of the feedback for these lines of code. The maximum size is 64 KB.</param>
        public CheckRunAnnotation(string filename, string blobHref, int startLine, int endLine, CheckWarningLevel warningLevel, string message)
        {
            Filename = filename;
            BlobHref = blobHref;
            StartLine = startLine;
            EndLine = endLine;
            WarningLevel = warningLevel;
            Message = message;
        }

        /// <summary>
        /// Required. The path of the file to add an annotation to. For example, assets/css/main.css.
        /// </summary>
        public string Filename { get; protected set; }

        /// <summary>
        /// Required. The file's full blob URL. You can find the blob_href in the response of the Get a single commit endpoint, by reading the blob_url from an element of the files array. You can also construct the blob URL from the head_sha, the repository, and the filename: https://github.com/:owner/:repo/blob/:head_sha/:filename.
        /// </summary>
        public string BlobHref { get; protected set; }

        /// <summary>
        /// Required. The start line of the annotation.
        /// </summary>
        public int StartLine { get; protected set; }

        /// <summary>
        /// Required. The end line of the annotation.
        /// </summary>
        public int EndLine { get; protected set; }

        /// <summary>
        /// Required. The warning level of the annotation. Can be one of notice, warning, or failure.
        /// </summary>
        public StringEnum<CheckWarningLevel> WarningLevel { get; protected set; }

        /// <summary>
        /// Required. A short description of the feedback for these lines of code. The maximum size is 64 KB.
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// The title that represents the annotation. The maximum size is 255 characters.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Details about this annotation. The maximum size is 64 KB.
        /// </summary>
        public string RawDetails { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Title: {0}, Filename: {1}, WarningLevel: {2}", Title ?? "<Untitled>", Filename, WarningLevel.DebuggerDisplay);
    }
}