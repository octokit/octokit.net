using System;
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
    /// Search commits
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
        /// Initializes a new instance of the <see cref="SearchCommitsRequest"/> class.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="name">The name.</param>
        public SearchCommitsRequest(string term, string owner, string name)
            : this(term)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            Repos.Add(owner, name);
        }

        /// <summary>
        /// Optional Sort field. Can only be by author-date or committer-date. 
        /// If not provided, results are sorted by best match.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/search/#search-commits
        /// </remarks>
        public CommitsSearchSort? SortField { get; set; }
        public override string Sort
        {
            get { return SortField.ToParameter(); }
        }

        private string QueryDateTime(DateTime input)
        {
            return input.ToString("yyyy-MM-DD");
        }

        /// <summary>
        /// Limits searches to a specific user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-within-a-users-or-organizations-repositories
        /// </remarks>
        public string User { get; set; }

        /// <summary>
        /// Limits searches to a specific author username
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string Author { get; set; }

        /// <summary>
        /// Limits searches to a specific committer username
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string Committer { get; set; }

        /// <summary>
        /// Limits searches to a specific author's name
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string AuthorName { get; set; }

        /// <summary>
        /// Limits results to a specific committers name
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string CommitterName { get; set; }

        /// <summary>
        /// Limits results to a specific author's email
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Limits results to a specific committers email
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-author-or-committer
        /// </remarks>
        public string CommitterEmail { get; set; }

        /// <summary>
        /// Limits searches to a specific organization.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-within-a-users-or-organizations-repositories
        /// </remarks>
        public string Organization { get; set; }

        /// <summary>
        /// Limits searches to match commits authored before the AuthoredDate
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-authored-or-committed-date
        /// </remarks>
        public DateTime AuthoredDate { get; set; }

        /// <summary>
        /// Limits searches to match commits committed before the CommittedDate
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-authored-or-committed-date
        /// </remarks>
        public DateTime CommittedDate { get; set; }

        /// <summary>
        /// If set, limits searches based on whether it is a merge commmit or not
        /// </summary>
        public bool? IsMerge { get; set; }

        /// <summary>
        /// Matches commits with the hash
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-hash
        /// </remarks>
        public string CommitHash { get; set; }

        /// <summary>
        /// Limits searches to match commits that are children of the Parent hash
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-parent
        /// </remarks>
        public string Parent { get; set; }

        /// <summary>
        /// Limits searches to match commits that refer to the Tree hash
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-by-tree
        /// </remarks>
        public string Tree { get; set; }

        /// <summary>
        /// Limits searches to a specific repository.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/en/github/searching-for-information-on-github/searching-commits#search-within-a-users-or-organizations-repositories
        /// </remarks>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public RepositoryCollection Repos { get; set; }

        [SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public override IReadOnlyList<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if (User.IsNotBlank())
            {
                parameters.Add($"user:{User}");
            }

            if (Author.IsNotBlank())
            {
                parameters.Add($"author:{Author}");
            }

            if (Committer.IsNotBlank())
            {
                parameters.Add($"committer:{Committer}");
            }

            if (AuthorName.IsNotBlank())
            {
                parameters.Add($"author-name:{AuthorName}");
            }

            if (CommitterName.IsNotBlank())
            {
                parameters.Add($"committer-name:{CommitterName}");
            }

            if (AuthorEmail.IsNotBlank())
            {
                parameters.Add($"author-email:{AuthorEmail}");
            }

            if (CommitterEmail.IsNotBlank())
            {
                parameters.Add($"committer-email:{CommitterEmail}");
            }

            if (AuthoredDate.IsNotDefault())
            {
                parameters.Add($"author-date:{QueryDateTime(AuthoredDate)}");
            }
            if (CommittedDate.IsNotDefault())
            {
                parameters.Add($"committer-date:{QueryDateTime(CommittedDate)}");
            }

            if (IsMerge.IsNotNull())
            {
                var isMerge = IsMerge == true ? "true" : "false";
                parameters.Add($"merge:{isMerge}");
            }

            if (CommitHash.IsNotBlank())
            {
                parameters.Add($"hash:{CommitHash}");
            }

            if (Parent.IsNotBlank())
            {
                parameters.Add($"parent:{Parent}");
            }

            if (Tree.IsNotBlank())
            {
                parameters.Add($"tree:{Tree}");
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

            if (Organization.IsNotBlank())
            {
                parameters.Add($"org:{Organization}");
            }

            return new ReadOnlyCollection<string>(parameters);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return $"Term: {Term} Sort: {Sort}";
            }
        }
    }

    public enum CommitsSearchSort
    {
        [Parameter(Value = "author-date")]
        AuthorDate,
        [Parameter(Value = "committer-date")]
        CommitterDate
    }
}
