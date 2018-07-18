using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckRunOutput
    {
        /// <summary>
        /// Constructs a CheckRunOutput request object
        /// </summary>
        /// <param name="title">Required. The title of the check run</param>
        /// <param name="summary">Required. The summary of the check run. This parameter supports Markdown</param>
        public NewCheckRunOutput(string title, string summary)
        {
            Title = title;
            Summary = summary;
        }

        /// <summary>
        /// Required. The title of the check run
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Required. The summary of the check run. This parameter supports Markdown
        /// </summary>
        public string Summary { get; protected set; }

        /// <summary>
        /// The details of the check run. This parameter supports Markdown
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Adds information from your analysis to specific lines of code. Annotations are visible in GitHub's pull request UI. For details about annotations in the UI, see "About status checks"
        /// </summary>
        public IReadOnlyList<NewCheckRunAnnotation> Annotations { get; set; }

        /// <summary>
        /// Adds images to the output displayed in the GitHub pull request UI
        /// </summary>
        public IReadOnlyList<NewCheckRunImage> Images { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "Title: {0}", Title);
    }
}