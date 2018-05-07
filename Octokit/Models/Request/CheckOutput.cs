using System.Collections.Generic;

namespace Octokit
{
    public sealed class CheckOutput
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public List<CheckAnnotation> Annotations { get; set; }
        public List<CheckImage> Images { get; set; }
    }
}