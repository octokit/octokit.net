using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckRunAnnotation
    {
        /// <summary>
        /// Constructs a CheckRunCreateAnnotation request object
        /// </summary>
        /// <param name="path">Required. The path of the file to add an annotation to. For example, assets/css/main.css</param>
        /// <param name="startLine">Required. The start line of the annotation</param>
        /// <param name="endLine">Required. The end line of the annotation</param>
        /// <param name="annotationLevel">Required. The level of the annotation. Can be one of notice, warning, or failure</param>
        /// <param name="message">Required. A short description of the feedback for these lines of code. The maximum size is 64 KB</param>
        public NewCheckRunAnnotation(string path, int startLine, int endLine, CheckAnnotationLevel annotationLevel, string message)
        {
            Path = path;
            StartLine = startLine;
            EndLine = endLine;
            AnnotationLevel = annotationLevel;
            Message = message;

            // Ensure legacy properties are explicitly null
#pragma warning disable CS0618 // Type or member is obsolete
            Filename = null;
            BlobHref = null;
            WarningLevel = null;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Constructs a CheckRunCreateAnnotation request object (using Filename, BlobHref and WarningLevel)
        /// </summary>
        /// <param name="filename">Required. The path of the file to add an annotation to. For example, assets/css/main.css</param>
        /// <param name="blobHref">Required. The file's full blob URL. You can find the blob_href in the response of the Get a single commit endpoint, by reading the blob_url from an element of the files array. You can also construct the blob URL from the head_sha, the repository, and the filename: https://github.com/:owner/:repo/blob/:head_sha/:filename </param>
        /// <param name="startLine">Required. The start line of the annotation</param>
        /// <param name="endLine">Required. The end line of the annotation</param>
        /// <param name="warningLevel">Required. The warning level of the annotation. Can be one of notice, warning, or failure</param>
        /// <param name="message">Required. A short description of the feedback for these lines of code. The maximum size is 64 KB</param>
        [Obsolete("This ctor taking Filename, BlobHref and WarningLevel is deprecated but may still be required on GitHub Enterprise 2.14")]
        public NewCheckRunAnnotation(string filename, string blobHref, int startLine, int endLine, CheckWarningLevel warningLevel, string message)
        {
            Filename = filename;
            BlobHref = blobHref;
            StartLine = startLine;
            EndLine = endLine;
            WarningLevel = warningLevel;
            Message = message;

            // Ensure new properties are explicitly null
            Path = null;
            AnnotationLevel = null;
        }

        /// <summary>
        /// Required. The path of the file to add an annotation to. For example, assets/css/main.css
        /// </summary>
        [Obsolete("This property is replaced with Path but may still be required on GitHub Enterprise 2.14")]
        public string Filename { get; protected set; }

        /// <summary>
        /// Required. The path of the file to add an annotation to. For example, assets/css/main.css
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Required. The file's full blob URL. You can find the blob_href in the response of the Get a single commit endpoint, by reading the blob_url from an element of the files array. You can also construct the blob URL from the head_sha, the repository, and the filename: https://github.com/:owner/:repo/blob/:head_sha/:filename
        /// </summary>
        [Obsolete("This property is deprecated but may still be required on GitHub Enterprise 2.14")]
        public string BlobHref { get; protected set; }

        /// <summary>
        /// Required. The start line of the annotation
        /// </summary>
        public int StartLine { get; protected set; }

        /// <summary>
        /// Required. The end line of the annotation
        /// </summary>
        public int EndLine { get; protected set; }

        /// <summary>
        /// Required. The start line of the annotation
        /// </summary>
        public int? StartColumn { get; set; }

        /// <summary>
        /// Required. The end line of the annotation
        /// </summary>
        public int? EndColumn { get; set; }

        /// <summary>
        /// Required. The warning level of the annotation. Can be one of notice, warning, or failure
        /// </summary>
        [Obsolete("This property is replaced with AnnotationLevel but may still be required on GitHub Enterprise 2.14")]
        public StringEnum<CheckWarningLevel>? WarningLevel { get; protected set; }

        /// <summary>
        /// Required. The level of the annotation. Can be one of notice, warning, or failure
        /// </summary>
        public StringEnum<CheckAnnotationLevel>? AnnotationLevel { get; protected set; }

        /// <summary>
        /// Required. A short description of the feedback for these lines of code. The maximum size is 64 KB
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// The title that represents the annotation. The maximum size is 255 characters
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Details about this annotation. The maximum size is 64 KB
        /// </summary>
        public string RawDetails { get; set; }

#pragma warning disable CS0618 // Type or member is obsolete
        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Path: {0}, StartLine: {1}, AnnotationLevel: {2}", Path ?? Filename, StartLine, AnnotationLevel?.DebuggerDisplay ?? WarningLevel?.DebuggerDisplay);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}