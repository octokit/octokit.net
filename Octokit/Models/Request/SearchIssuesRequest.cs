using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    /// <summary>
    /// Searching Issues
    /// </summary>
    public class SearchIssuesRequest : BaseSearchRequest
    {
        public SearchIssuesRequest(string term) : base(term) { }

        /// <summary>
        /// For http://developer.github.com/v3/search/#search-issues
        /// Optional Sort field. One of comments, created, or updated. If not provided, results are sorted by best match.
        /// </summary>
        public IssueSearchSort? SortField { get; set; }

        public override string Sort
        {
            get { return SortField.ToParameter(); }
        }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#search-in
        /// </summary>
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
        /// https://help.github.com/articles/searching-issues#type
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public IssueTypeQualifier? Type { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#assignee
        /// </summary>
        public string Assignee { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#mentions
        /// </summary>
        public string Mentions { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#commenter
        /// </summary>
        public string Commenter { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#involves
        /// </summary>
        public string Involves { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#state
        /// </summary>
        public ItemState? State { get; set; }

        private IEnumerable<string> _labels;
        /// <summary>
        /// https://help.github.com/articles/searching-issues#labels
        /// </summary>
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
        /// https://help.github.com/articles/searching-issues#language
        /// </summary>
        public Language? Language { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#created-and-last-updated
        /// </summary>
        public DateRange Created { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#created-and-last-updated
        /// </summary>
        public DateRange Updated { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#comments
        /// </summary>
        public Range Comments { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#users-organizations-and-repositories
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// https://help.github.com/articles/searching-issues#users-organizations-and-repositories
        /// </summary>
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

            if (State != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "state:{0}", State));
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

            return parameters;
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
