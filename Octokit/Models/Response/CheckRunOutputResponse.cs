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
        public string Title { get; private set; }

        /// <summary>
        /// The summary of the check run
        /// </summary>
        public string Summary { get; private set; }

        /// <summary>
        /// The details of the check run
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// The number of annotation entries for the check run (use <see cref="ICheckRunsClient.GetAllAnnotations(string, string, long)"/> to get annotation details)
        /// </summary>
        public long AnnotationsCount { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "Title: {0}", Title);
    }
}
