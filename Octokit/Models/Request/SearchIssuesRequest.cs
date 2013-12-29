using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Searching Issues
    /// </summary>
    public class SearchIssuesRequest
    {
        public SearchIssuesRequest(string term)
        {
            Ensure.ArgumentNotNullOrEmptyString(term, "term");

            Term = term;
            Page = 1;
            PerPage = 100;
            Order = SortDirection.Descending;
        }

        /// <summary>
        /// The search terms. This can be any combination of the supported repository search parameters:
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// For http://developer.github.com/v3/search/#search-issues
        /// Optional Sort field. One of comments, created, or updated. If not provided, results are sorted by best match.
        /// </summary>
        public IssueSearchSort? Sort { get; set; }

        /// <summary>
        /// Optional Sort order if sort parameter is provided. One of asc or desc; the default is desc.
        /// </summary>
        public SortDirection Order { get; set; }

        /// <summary>
        /// Page of paginated results
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PerPage { get; set; }

        private IEnumerable<IssueInQualifier> _inQualifier;
        public IEnumerable<IssueInQualifier> In
        {
            get { return _inQualifier; }
            set
            {
                if(value != null && value.Any())
                {
                    _inQualifier = value.Distinct().ToList();
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public IssueTypeQualifier? Type { get; set; }

        public string Author { get; set; }

        public string Assignee { get; set; }

        public string Mentions { get; set; }

        public string Commenter { get; set; }

        public string Involves { get; set; }

        public ItemState? State { get; set; }

        private IEnumerable<string> _labels;

        public IEnumerable<string> Labels 
        {
            get { return _labels; } 
            set
            {
                if(value != null && value.Any())
                {
                    _labels = value.Distinct().ToList();
                }
            } 
        }

        public Language? Language { get; set; }

        public DateRange Created { get; set; }

        public DateRange Updated { get; set; }

        public Range Comments { get; set; }

        public string User { get; set; }

        public string Repo { get; set; }

        public string MergeParameters()
        {
            var parameters = new List<string>();

            if(In != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "in:{0}", String.Join(",", In)));
            }

            if(Type != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "type:{0}", Type));
            }

            if(Author.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "author:{0}", Author));
            }

            if(Assignee.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "assignee:{0}", Assignee));
            }

            if(Mentions.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "mentions:{0}", Mentions));
            }

            if (Commenter.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "commenter:{0}", Commenter));
            }

            if(Involves.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "involves:{0}", Involves));
            }

            if(State != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "state:{0}", State));
            }

            if(Labels != null)
            {
                foreach(var label in Labels)
                {
                    parameters.Add(String.Format(CultureInfo.InvariantCulture, "label:{0}", label));
                }
            }

            if(Language != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "language:{0}", Language));
            }

            if(Created != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "created:{0}", Created));
            }

            if(Updated != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "udpated:{0}", Updated));
            }

            if(Comments != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "comments:{0}", Comments));
            }

            if(User.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "user:{0}", User));
            }

            if(Repo.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "repo:{0}", Repo));
            }

            return String.Join("+", parameters);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public IDictionary<string, string> Parameters
        {
            get
            {
                var d = new Dictionary<string, string>();
                d.Add("page", Page.ToString());
                d.Add("per_page", PerPage.ToString());
                d.Add("sort", Sort.ToString());
                d.Add("q", Term + " " + MergeParameters());
                return d;
            }
        }
    }
    public enum IssueSearchSort
    {
        /// <summary>
        /// search by number of comments
        /// </summary>
        Comments,
        /// <summary>
        /// search by created
        /// </summary>
        Created,
        /// <summary>
        /// search by last updated
        /// </summary>
        Updated
    }

    public enum IssueInQualifier
    {
        Title,
        Body,
        Comment
    }

    public enum IssueTypeQualifier
    {
        PR,
        Issue
    }
}
