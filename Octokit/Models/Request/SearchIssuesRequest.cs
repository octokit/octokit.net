using System.Collections.ObjectModel;
using System.Diagnostics;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    /// <summary>
    /// Searching Issues
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchIssuesRequest : BaseSearchRequest
    {
        public SearchIssuesRequest(string term) : base(term) { }

        /// <summary>
        /// Optional Sort field. One of comments, created, or updated. 
        /// If not provided, results are sorted by best match.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/search/#search-issues
        /// </remarks>
        public IssueSearchSort? SortField { get; set; }

        public override string Sort
        {
            get { return SortField.ToParameter(); }
        }

        /// <summary>
        /// Qualifies which fields are searched. With this qualifier you can restrict 
        /// the search to just the title, body, comments, or any combination of these.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#search-in
        /// </remarks>
        private IEnumerable<IssueInQualifier> _inQualifier;
        public IEnumerable<IssueInQualifier> In
        {
            get { return _inQualifier; }
            set
            {
                if (value != null && value.Any())
                {
                    _inQualifier = value.Distinct().ToList();
                }
            }
        }

        /// <summary>
        /// With this qualifier you can restrict the search to issues or pull request only.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#type
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public IssueTypeQualifier? Type { get; set; }

        /// <summary>
        /// Finds issues created by a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#author
        /// </remarks>
        public string Author { get; set; }

        /// <summary>
        /// Finds issues that are assigned to a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#assignee
        /// </remarks>
        public string Assignee { get; set; }

        /// <summary>
        /// Finds issues that mention a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#mentions
        /// </remarks>
        public string Mentions { get; set; }

        /// <summary>
        /// Finds issues that a certain user commented on.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#commenter
        /// </remarks>
        public string Commenter { get; set; }

        /// <summary>
        /// Finds issues that were either created by a certain user, assigned to that user, 
        /// mention that user, or were commented on by that user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#involves
        /// </remarks>
        public string Involves { get; set; }

        /// <summary>
        /// Filter issues based on whether they’re open or closed.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#state
        /// </remarks>
        public ItemState? State { get; set; }

        private IEnumerable<string> _labels;
        /// <summary>
        /// Filters issues based on their labels.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#labels
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
        /// Searches for issues within repositories that match a certain language.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#language
        /// </remarks>
        public Language? Language { get; set; }

        /// <summary>
        /// Filters issues based on times of creation.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#created-and-last-updated
        /// </remarks>
        public DateRange Created { get; set; }

        /// <summary>
        /// Filters issues based on times when they were last updated.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#created-and-last-updated
        /// </remarks>
        public DateRange Updated { get; set; }

        /// <summary>
        /// Filters issues based on the quantity of comments.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#comments
        /// </remarks>
        public Range Comments { get; set; }

        /// <summary>
        /// Limits searches to a specific user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#users-organizations-and-repositories
        /// </remarks>
        public string User { get; set; }

        /// <summary>
        /// Limits searches to a specific repository.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#users-organizations-and-repositories
        /// </remarks>
        public string Repo { get; set; }

        public override IReadOnlyCollection<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if (In != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "in:{0}", 
                    String.Join(",", In.Select(i => i.ToParameter()))));
            }

            if (Type != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "type:{0}", 
                    Type.ToParameter()));
            }

            if (Author.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "author:{0}", Author));
            }

            if (Assignee.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "assignee:{0}", Assignee));
            }

            if (Mentions.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "mentions:{0}", Mentions));
            }

            if (Commenter.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "commenter:{0}", Commenter));
            }

            if (Involves.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "involves:{0}", Involves));
            }

            if (State.HasValue)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "state:{0}", State.Value.ToParameter()));
            }

            if (Labels != null)
            {
                foreach (var label in Labels)
                {
                    parameters.Add(String.Format(CultureInfo.InvariantCulture, "label:{0}", label));
                }
            }

            if (Language != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "language:{0}", Language));
            }

            if (Created != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "created:{0}", Created));
            }

            if (Updated != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "updated:{0}", Updated));
            }

            if (Comments != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "comments:{0}", Comments));
            }

            if (User.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "user:{0}", User));
            }

            if (Repo.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "repo:{0}", Repo));
            }

            return new ReadOnlyCollection<string>(parameters);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Term: {0}", Term);
            }
        }
    }

    public enum IssueSearchSort
    {
        /// <summary>
        /// search by number of comments
        /// </summary>
        [Parameter(Value = "comments")]
        Comments,
        /// <summary>
        /// search by created
        /// </summary>
        [Parameter(Value = "created")]
        Created,
        /// <summary>
        /// search by last updated
        /// </summary>
        [Parameter(Value = "updated")]
        Updated
    }

    public enum IssueInQualifier
    {
        [Parameter(Value = "title")]
        Title,
        [Parameter(Value = "body")]
        Body,
        [Parameter(Value = "comment")]
        Comment
    }

    public enum IssueTypeQualifier
    {
        [Parameter(Value = "pr")]
        PR,
        [Parameter(Value = "issue")]
        Issue
    }
}
