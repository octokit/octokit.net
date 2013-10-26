using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class NotificationInfo
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string LatestCommentUrl { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "Matches the property name used by the API")]
        public string Type { get; set; }
    }
}
