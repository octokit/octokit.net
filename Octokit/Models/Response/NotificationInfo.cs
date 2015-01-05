using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class NotificationInfo
    {
        public string Title { get; protected set; }

        public string Url { get; protected set; }

        public string LatestCommentUrl { get; protected set; }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "Matches the property name used by the API")]
        public string Type { get; protected set; }
    }
}
