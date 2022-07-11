using Octokit.Internal;
using System;
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
    public class SearchIssuesRequest : BaseSearchRequest
    {
        /// <summary>
        /// Search without specifying a keyword
        /// </summary>
        public SearchIssuesRequest()
        {
            Repos = new RepositoryCollection();
        }

        /// <summary>
        /// Search using a specify keyword
        /// </summary>
        /// <param name="term">The term to filter on</param>
        public SearchIssuesRequest(string term) : base(term)
        {
            Repos = new RepositoryCollection();
        }

        /// <summary>
        /// Optional Sort field. One of comments, created, updated or merged
        /// If not provided, results are sorted by best match.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#sort-the-results
        /// </remarks>
        public IssueSearchSort? SortField { get; set; }

        public override string Sort
        {
            get { return SortField.ToParameter(); }
        }

        /// <summary>
        /// With this qualifier you can restrict the search to issues or pull request only.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-issues-or-pull-requests
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public IssueTypeQualifier? Type { get; set; }

        /// <summary>
        /// Qualifies which fields are searched. With this qualifier you can restrict
        /// the search to just the title, body, comments, or any combination of these.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#scope-the-search-fields
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
        /// Finds issues created by a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-the-author-of-an-issue-or-pull-request
        /// </remarks>
        public string Author { get; set; }

        /// <summary>
        /// Finds issues that are assigned to a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-the-assignee-of-an-issue-or-pull-request
        /// </remarks>
        public string Assignee { get; set; }

        /// <summary>
        /// Finds issues that mention a certain user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-a-mentioned-user-within-an-issue-or-pull-request
        /// </remarks>
        public string Mentions { get; set; }

        /// <summary>
        /// Finds issues that a certain user commented on.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-a-commenter-within-an-issue-or-pull-request
        /// </remarks>
        public string Commenter { get; set; }

        /// <summary>
        /// Finds issues that were either created by a certain user, assigned to that user,
        /// mention that user, or were commented on by that user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-a-user-thats-involved-within-an-issue-or-pull-request
        /// </remarks>
        public string Involves { get; set; }

        /// <summary>
        /// Finds issues that @mention a team within the organization
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-a-team-thats-mentioned-within-an-issue-or-pull-request
        /// </remarks>
        public string Team { get; set; }

        /// <summary>
        /// Filter issues based on whether they’re open or closed.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-whether-an-issue-or-pull-request-is-open
        /// </remarks>
        public ItemState? State { get; set; }

        private IEnumerable<string> _labels;
        /// <summary>
        /// Filters issues based on the labels assigned.
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
        /// Searches for issues based on missing metadata.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-missing-metadata-on-an-issue-or-pull-request
        /// </remarks>
        public IssueNoMetadataQualifier? No { get; set; }

        /// <summary>
        /// Searches for issues in repositories that match a certain language.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-by-the-main-language-of-a-repository
        /// </remarks>
        public Language? Language { get; set; }

        private IEnumerable<IssueIsQualifier> _is;

        /// <summary>
        /// Searches for issues using a more human syntax covering options like state, type, merged status, private/public repository
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-the-state-of-an-issue-or-pull-request
        /// </remarks>
        public IEnumerable<IssueIsQualifier> Is
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

        /// <summary>
        /// Filters issues based on times of creation.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-when-an-issue-or-pull-request-was-created-or-last-updated
        /// </remarks>
        public DateRange Created { get; set; }

        /// <summary>
        /// Filters issues based on times when they were last updated.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-when-an-issue-or-pull-request-was-created-or-last-updated
        /// </remarks>
        public DateRange Updated { get; set; }

        /// <summary>
        /// Filters pull requests based on times when they were last merged.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-when-a-pull-request-was-merged
        /// </remarks>
        public DateRange Merged { get; set; }

        /// <summary>
        /// Filters pull requests based on the status of the commits.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-commit-status
        /// </remarks>
        public CommitState? Status { get; set; }

        /// <summary>
        /// Filters pull requests based on the branch they came from.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-branch-names
        /// </remarks>
        public string Head { get; set; }

        /// <summary>
        /// Filters pull requests based on the branch they are merging into.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-branch-names
        /// </remarks>
        public string Base { get; set; }

        /// <summary>
        /// Filters issues based on times when they were last closed.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-when-an-issue-or-pull-request-was-closed
        /// </remarks>
        public DateRange Closed { get; set; }

        /// <summary>
        /// Filters issues based on the quantity of comments.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues#comments
        /// </remarks>
        public Range Comments { get; set; }

        /// <summary>
        /// Limits searches to repositories owned by a certain user or organization.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-within-a-users-or-organizations-repositories
        /// </remarks>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the milestone to filter issues based on
        /// </summary>
        public string Milestone { get; set; }

        /// <summary>
        /// Filters issues or pull requests based on whether they are in an archived repository.
        /// </summary>
        public bool? Archived { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public RepositoryCollection Repos { get; set; }

        public SearchIssuesRequestExclusions Exclusions { get; set; }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override IReadOnlyList<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if (Type != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "type:{0}",
                    Type.ToParameter()));
            }

            if (In != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "in:{0}",
                    string.Join(",", In.Select(i => i.ToParameter()))));
            }

            if (Author.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "author:{0}", Author));
            }

            if (Assignee.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "assignee:{0}", Assignee));
            }

            if (Mentions.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "mentions:{0}", Mentions));
            }

            if (Commenter.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "commenter:{0}", Commenter));
            }

            if (Involves.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "involves:{0}", Involves));
            }

            if (Team.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "team:{0}", Team));
            }

            if (State.HasValue)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "state:{0}", State.Value.ToParameter()));
            }

            if (Labels != null)
            {
                parameters.AddRange(Labels.Select(label => string.Format(CultureInfo.InvariantCulture, "label:\"{0}\"", label)));
            }

            if (No.HasValue)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "no:{0}", No.Value.ToParameter()));
            }

            if (Language != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "language:\"{0}\"", Language.ToParameter()));
            }

            if (Is != null)
            {
                parameters.AddRange(Is.Select(x => string.Format(CultureInfo.InvariantCulture, "is:{0}", x.ToParameter())));
            }

            if (Created != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "created:{0}", Created));
            }

            if (Updated != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "updated:{0}", Updated));
            }

            if (Merged != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "merged:{0}", Merged));
            }

            if (Status.HasValue)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "status:{0}", Status.Value.ToParameter()));
            }

            if (Head.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "head:{0}", Head));
            }

            if (Base.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "base:{0}", Base));
            }

            if (Closed != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "closed:{0}", Closed));
            }

            if (Comments != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "comments:{0}", Comments));
            }

            if (User.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "user:{0}", User));
            }

            if (Repos.Any())
            {
                var invalidFormatRepos = Repos.Where(x => !x.IsNameWithOwnerFormat());
                if (invalidFormatRepos.Any())
                {
                    throw new RepositoryFormatException(invalidFormatRepos);
                }

                parameters.AddRange(Repos.Select(x => string.Format(CultureInfo.InvariantCulture, "repo:{0}", x)));
            }

            if (Milestone.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "milestone:\"{0}\"", Milestone.EscapeDoubleQuotes()));
            }

            if (Archived != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "archived:{0}", Archived.ToString().ToLower()));
            }

            // Add any exclusion parameters
            if (Exclusions != null)
            {
                parameters.AddRange(Exclusions.MergedQualifiers());
            }

            return new ReadOnlyCollection<string>(parameters);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Search: {0} {1}", Term, string.Join(" ", MergedQualifiers()));
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
        Updated,
        /// <summary>
        /// search by last merged
        /// </summary>
        [Parameter(Value = "merged")]
        Merged
    }

    public enum IssueTypeQualifier
    {
        [Parameter(Value = "pr")]
        PullRequest,
        [Parameter(Value = "issue")]
        Issue
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

    public enum IssueIsQualifier
    {
        [Parameter(Value = "open")]
        Open,
        [Parameter(Value = "closed")]
        Closed,
        [Parameter(Value = "merged")]
        Merged,
        [Parameter(Value = "unmerged")]
        Unmerged,
        [Parameter(Value = "pr")]
        PullRequest,
        [Parameter(Value = "issue")]
        Issue,
        [Parameter(Value = "private")]
        Private,
        [Parameter(Value = "public")]
        Public
    }

    public enum IssueNoMetadataQualifier
    {
        [Parameter(Value = "label")]
        Label,
        [Parameter(Value = "milestone")]
        Milestone,
        [Parameter(Value = "assignee")]
        Assignee,
        [Parameter(Value = "project")]
        Project
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryCollection : Collection<string>
    {
        public void Add(string owner, string name)
        {
            Add(GetRepositoryName(owner, name));
        }

        public bool Contains(string owner, string name)
        {
            return Contains(GetRepositoryName(owner, name));
        }

        public bool Remove(string owner, string name)
        {
            return Remove(GetRepositoryName(owner, name));
        }

        static string GetRepositoryName(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", owner, name);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Repositories: {0}", Count);
            }
        }
    }
}
