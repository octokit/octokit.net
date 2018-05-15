using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunOutput
    {
        public CheckRunOutput()
        {
        }

        public CheckRunOutput(string title, string summary, string text, IReadOnlyList<CheckRunAnnotation> annotations, IReadOnlyList<CheckRunImage> images)
        {
            Title = title;
            Summary = summary;
			Text = text;
            Annotations = annotations;
            Images = images;
        }

        public string Title { get; protected set; }
        public string Summary { get; protected set; }
        public string Text { get; protected set; }
        public IReadOnlyList<CheckRunAnnotation> Annotations { get; protected set; }
        public IReadOnlyList<CheckRunImage> Images { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "Title: {0}", Title);
    }
}