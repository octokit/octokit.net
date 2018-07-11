using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunOutputResponse
    {
        public CheckRunOutputResponse()
        {
        }

        public CheckRunOutputResponse(string title, string summary, string text, long annotationsCount)
        {
            Title = title;
            Summary = summary;
			Text = text;
            AnnotationsCount = annotationsCount;
        }

        /// <summary>
        /// The title of the check run
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The summary of the check run
        /// </summary>
        public string Summary { get; protected set; }

        /// <summary>
        /// The details of the check run
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// The number of annotations (use ICheckRunsClient.GetAllAnnotations() to load annotations)
        /// </summary>
        public long AnnotationsCount { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "Title: {0}", Title);
    }
}