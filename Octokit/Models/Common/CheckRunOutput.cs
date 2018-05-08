using System.Collections.Generic;

namespace Octokit
{
    public class CheckRunOutput
    {
        public CheckRunOutput()
        {
        }

        public CheckRunOutput(string title, string summary, IReadOnlyList<CheckRunAnnotation> annotations, IReadOnlyList<CheckRunImage> images)
        {
            Title = title;
            Summary = summary;
            Annotations = annotations;
            Images = images;
        }

        public string Title { get; protected set; }
        public string Summary { get; protected set; }
        public IReadOnlyList<CheckRunAnnotation> Annotations { get; protected set; }
        public IReadOnlyList<CheckRunImage> Images { get; protected set; }
    }
}