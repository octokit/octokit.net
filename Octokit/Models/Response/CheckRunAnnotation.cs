using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunAnnotation
    {
        public CheckRunAnnotation()
        {
        }

        public CheckRunAnnotation(string filename, string blobHref, int startLine, int endLine, CheckWarningLevel warningLevel, string message, string title, string rawDetails)
        {
            Filename = filename;
            BlobHref = blobHref;
            StartLine = startLine;
            EndLine = endLine;
            WarningLevel = warningLevel;
            Message = message;
            Title = title;
            RawDetails = rawDetails;
        }

        /// <summary>
        /// The path of the file the annotation refers to
        /// </summary>
        public string Filename { get; protected set; }

        /// <summary>
        /// The file's full blob URL
        /// </summary>
        public string BlobHref { get; protected set; }

        /// <summary>
        /// The start line of the annotation
        /// </summary>
        public int StartLine { get; protected set; }

        /// <summary>
        /// The end line of the annotation
        /// </summary>
        public int EndLine { get; protected set; }

        /// <summary>
        /// The warning level of the annotation. Can be one of notice, warning, or failure
        /// </summary>
        public StringEnum<CheckWarningLevel> WarningLevel { get; protected set; }

        /// <summary>
        /// A short description of the feedback for these lines of code
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// The title that represents the annotation
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Details about this annotation
        /// </summary>
        public string RawDetails { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Filename: {0}, StartLine: {1}, WarningLevel: {2}", Filename, StartLine, WarningLevel.DebuggerDisplay);
    }
}