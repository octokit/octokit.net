using System;
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

        public CheckRunAnnotation(string path, string blobHref, int startLine, int endLine, int? startColumn, int? endColumn, CheckAnnotationLevel? annotationLevel, string message, string title, string rawDetails)
        {
            Path = path;
            BlobHref = blobHref;
            StartLine = startLine;
            EndLine = endLine;
            StartColumn = startColumn;
            EndColumn = endColumn;
            AnnotationLevel = annotationLevel;
            Message = message;
            Title = title;
            RawDetails = rawDetails;

            // Ensure legacy properties are explicitly null
#pragma warning disable CS0618 // Type or member is obsolete
            Filename = null;
            WarningLevel = null;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [Obsolete("This ctor taking Filename, BlobHref and WarningLevel is deprecated but may still be required on GitHub Enterprise 2.14")]
        public CheckRunAnnotation(string filename, string path, string blobHref, int startLine, int endLine, int? startColumn, int? endColumn, CheckWarningLevel? warningLevel, CheckAnnotationLevel? annotationLevel, string message, string title, string rawDetails)
        {
            Filename = filename;
            Path = path;
            BlobHref = blobHref;
            StartLine = startLine;
            EndLine = endLine;
            StartColumn = startColumn;
            EndColumn = endColumn;
            WarningLevel = warningLevel;
            AnnotationLevel = annotationLevel;
            Message = message;
            Title = title;
            RawDetails = rawDetails;
        }

        /// <summary>
        /// The path of the file the annotation refers to
        /// </summary>
        [Obsolete("This property is replaced with Path but may still be required on GitHub Enterprise 2.14")]
        public string Filename { get; private set; }

        /// <summary>
        /// The path of the file the annotation refers to
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The file's full blob URL
        /// </summary>
        public string BlobHref { get; private set; }

        /// <summary>
        /// The start line of the annotation
        /// </summary>
        public int StartLine { get; private set; }

        /// <summary>
        /// The end line of the annotation
        /// </summary>
        public int EndLine { get; private set; }

        /// <summary>
        /// The start line of the annotation
        /// </summary>
        public int? StartColumn { get; private set; }

        /// <summary>
        /// The end line of the annotation
        /// </summary>
        public int? EndColumn { get; private set; }

        /// <summary>
        /// The warning level of the annotation. Can be one of notice, warning, or failure
        /// </summary>
        [Obsolete("This property is replaced with AnnotationLevel but may still be required on GitHub Enterprise 2.14")]
        public StringEnum<CheckWarningLevel>? WarningLevel { get; private set; }

        /// <summary>
        /// The level of the annotation. Can be one of notice, warning, or failure
        /// </summary>
        public StringEnum<CheckAnnotationLevel>? AnnotationLevel { get; private set; }

        /// <summary>
        /// A short description of the feedback for these lines of code
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The title that represents the annotation
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Details about this annotation
        /// </summary>
        public string RawDetails { get; private set; }

#pragma warning disable CS0618 // Type or member is obsolete
        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Path: {0}, StartLine: {1}, WarningLevel: {2}", Path ?? Filename, StartLine, AnnotationLevel?.DebuggerDisplay ?? WarningLevel?.DebuggerDisplay);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
