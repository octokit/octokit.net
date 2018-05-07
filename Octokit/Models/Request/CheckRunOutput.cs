using System.Collections.Generic;

namespace Octokit
{
    public sealed class CheckRunOutput
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public List<CheckRunAnnotation> Annotations { get; set; }
        public List<CheckRunImage> Images { get; set; }
    }
}