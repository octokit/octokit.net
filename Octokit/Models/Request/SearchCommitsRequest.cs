using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Searching Code/Files
    /// https://developer.github.com/v3/search/#search-commits
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCommitsRequest : BaseSearchRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommitsRequest"/> class.
        /// </summary>
        public SearchCommitsRequest() : base()
        {
            Repos = new RepositoryCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommitsRequest"/> class.
        /// </summary>
        /// <param name="term">The search term.</param>
        public SearchCommitsRequest(string term) : base(term)
        {
            Repos = new RepositoryCollection();
        }

        /// <summary>
        /// Optional Sort field. Can only be indexed, which indicates how recently 
        /// a file has been indexed by the GitHub search infrastructure. 
        /// If not provided, results are sorted by best match.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/search/#search-commits
        /// </remarks>
        public CommitSearchSort? SortField { get; set; }
        public override string Sort
        {
            get { return SortField.ToParameter(); }
        }

        /// <summary>
        /// Finds commits authored by specified name
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string Author;

        /// <summary>
        /// Finds commits commited by specified name
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string Committer;

        /// <summary>
        /// Finds commits what have specified value in author name
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string AuthorName;

        /// <summary>
        /// Finds commits what have specified value in committer name
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string CommitterName;

        /// <summary>
        /// Finds commits authored by specified email
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string AuthorEmail;

        /// <summary>
        /// Finds commits committed by specified email
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string CommitterEmail;

        /// <summary>
        /// Filters committs authored based on time specified
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-authored-or-committed-date
        /// </remarks>
        public DateRange AuthorDate;

        /// <summary>
        /// Filters committs committed based on time specified
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-authored-or-committed-date
        /// </remarks>
        public DateRange CommitterDate;

        /// <summary>
        /// Filter merged commits
        /// </summary>
        /// <range>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#filter-merge-commits
        /// </range>
        public bool? Merge;

        /// <summary>
        /// Search by committ hash.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-hash
        /// </remarks>
        public string CommittHash;

        /// <summary>
        /// Search by parent hash.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-parent
        /// </remarks>
        public string ParentHash;

        /// <summary>
        /// Search by tree hash.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-tree
        /// </remarks>
        public string TreeHash;

        /// <summary>
        /// Limits searches to a specific user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-within-a-users-or-organizations-repositories
        /// </remarks>
        public string User { get; set; }

        /// <summary>
        /// Limits searches to a specific organization.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-within-a-users-or-organizations-repositories
        /// </remarks>
        public string Organization { get; set; }

        /// <summary>
        /// Limits searches to a specific repository.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-within-a-users-or-organizations-repositories
        /// </remarks>
        // [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public RepositoryCollection Repos { get; set; }

        private IEnumerable<CommittIsQualifier> _is;

        /// <summary>
        /// Searches for Committs using a more human syntax covering options like private/public repository
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-the-state-of-an-issue-or-pull-request
        /// </remarks>
        public IEnumerable<CommittIsQualifier> Is
        {
            get { return _is; }
            set
            {
                if (value != null && value.Any())
                {
                    _is = value.Distinct().ToList();
                }
            }
        }

        public override IReadOnlyList<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if(Author.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "author:{0}", Author));
            }
            if(Committer.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "committer:{0}", Committer));
            }
            if (AuthorName.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "author-name:{0}", AuthorName));
            }
            if (Committer.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "committer-name:{0}", CommitterName));
            }
            if (AuthorEmail.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "author-email:{0}", AuthorEmail));
            }
            if (CommitterEmail.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "committer-email:{0}", CommitterEmail));
            }

            if (AuthorDate != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "author-date:{0}", AuthorDate));
            }
            if (CommitterDate != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "committer-date:{0}", CommitterDate));
            }

            if (Merge != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "merge:{0}", Merge));
            }

            if (CommittHash.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "hash:{0}", CommittHash));
            }
            if (ParentHash.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "parent:{0}", ParentHash));
            }
            if (TreeHash.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "tree:{0}", TreeHash));
            }

            if (User.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "user:{0}", User));
            }
            if (Organization.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "org:{0}", Organization));
            }
            if (Repos.Any())
            {
                var invalidFormatRepos = Repos.Where(x => !x.IsNameWithOwnerFormat());
                if (invalidFormatRepos.Any())
                {
                    throw new RepositoryFormatException(invalidFormatRepos);
                }

                parameters.Add(
                    string.Join("+", Repos.Select(x => "repo:" + x)));
            }

            if (Is != null)
            {
                parameters.AddRange(Is.Select(x => string.Format(CultureInfo.InvariantCulture, "is:{0}", x.ToParameter())));
            }
            

            return parameters;
        }

        public enum CommittIsQualifier
        {
            [Parameter(Value = "private")]
            Private,
            [Parameter(Value = "public")]
            Public
        }

        public enum CommitSearchSort
        {
            /// <summary>
            /// Sort by 
            /// </summary>
            [Parameter(Value = "author-date")]
            AuthorDate,
            /// <summary>
            /// Sort by
            /// </summary>
            [Parameter(Value = "committer-date")]
            CommiterDate

        }
    }
}
