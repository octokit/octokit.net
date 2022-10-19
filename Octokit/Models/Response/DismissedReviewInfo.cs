using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DismissedReviewInfo
    {
        public DismissedReviewInfo() { }

        public DismissedReviewInfo(PullRequestReviewState state, string reviewId, string dismissalMessage, string dismissalCommitId)
        {
            State = state;
            ReviewId = reviewId;
            DismissalMessage = dismissalMessage;
            DismissalCommitId = dismissalCommitId;
        }

        public StringEnum<PullRequestReviewState> State { get; private set; }
        public string ReviewId { get; private set; }
        public string DismissalMessage { get; private set; }
        public string DismissalCommitId { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "ReviewId: {0}", ReviewId); }
        }
    }
}
