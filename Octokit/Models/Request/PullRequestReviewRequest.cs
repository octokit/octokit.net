using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to add and delete pull request review requests.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/pulls/review_requests/#create-a-review-request
    /// API: https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewRequest
    {
        public PullRequestReviewRequest(IReadOnlyList<string> reviewers)
        {
            Reviewers = reviewers;
        }

        public IReadOnlyList<string> Reviewers { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Reviewers: {0}", string.Join(", ", Reviewers)); }
        }
    }
}