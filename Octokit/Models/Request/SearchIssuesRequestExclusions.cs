using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Searching Issues
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchIssuesRequestExclusions
    {
        /// <summary>
        /// Exclusions for Issue Search
        /// </summary>
        public SearchIssuesRequestExclusions()
        {
        }

        /// <summary>
        /// Excludes issues created by a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-the-author-of-an-issue-or-pull-request
        /// </remarks>
        public string Author { get; set; }

        /// <summary>
        /// Excludes issues that are assigned to a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-the-assignee-of-an-issue-or-pull-request
        /// </remarks>
        public string Assignee { get; set; }

        /// <summary>
        /// Excludes issues that mention a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-a-mentioned-user-within-an-issue-or-pull-request
        /// </remarks>
        public string Mentions { get; set; }

        /// <summary>
        /// Excludes issues that a certain user commented on.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-a-commenter-within-an-issue-or-pull-request
        /// </remarks>
        public string Commenter { get; set; }

        /// <summary>
        /// Excludes issues that were either created by a certain user, assigned to that user, 
        /// mention that user, or were commented on by that user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-a-user-thats-involved-within-an-issue-or-pull-request
        /// </remarks>
        public string Involves { get; set; }

        /// <summary>
        /// Excludes issues based on open/closed state.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-whether-an-issue-or-pull-request-is-open
        /// </remarks>
        public ItemState? State { get; set; }

        private IEnumerable<string> _labels;
        /// <summary>
        /// Excludes issues based on the labels assigned.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-the-labels-on-an-issue
        /// </remarks>
        public IEnumerable<string> Labels
        {
            get { return _labels; }
            set
            {
                if (value != null && value.Any())
                {
                    _labels = value.Distinct().ToList();
                }
            }
        }

        /// <summary>
        /// Excludes issues in repositories that match a certain language.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-the-main-language-of-a-repository
        /// </remarks>
        public Language? Language { get; set; }

        /// <summary>
        /// Excludes pull requests based on the status of the commits.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-commit-status
        /// </remarks>
        public CommitState? Status { get; set; }

        /// <summary>
        /// Excludes pull requests based on the branch they came from.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-branch-names
        /// </remarks>
        public string Head { get; set; }

        /// <summary>
        /// Excludes pull requests based on the branch they are merging into.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-branch-names
        /// </remarks>
        public string Base { get; set; }

        /// <summary>
        /// Excludes issues which target the specified milestone.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues-and-pull-requests/#search-by-milestone-on-an-issue-or-pull-request
        /// </remarks>
        public string Milestone { get; set; }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public IReadOnlyList<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if (Author.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-author:{0}", Author));
            }

            if (Assignee.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-assignee:{0}", Assignee));
            }

            if (Mentions.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-mentions:{0}", Mentions));
            }

            if (Commenter.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-commenter:{0}", Commenter));
            }

            if (Involves.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-involves:{0}", Involves));
            }

            if (State.HasValue)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-state:{0}", State.Value.ToParameter()));
            }

            if (Labels != null)
            {
                parameters.AddRange(Labels.Select(label => string.Format(CultureInfo.InvariantCulture, "-label:{0}", label)));
            }

            if (Language != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-language:\"{0}\"", Language.ToParameter()));
            }

            if (Status.HasValue)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-status:{0}", Status.Value.ToParameter()));
            }

            if (Head.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-head:{0}", Head));
            }

            if (Base.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-base:{0}", Base));
            }

            if (Milestone.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "-milestone:\"{0}\"", Milestone.EscapeDoubleQuotes()));
            }

            return new ReadOnlyCollection<string>(parameters);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Exclusions: {0}", string.Join(" ", MergedQualifiers()));
            }
        }
    }
}
