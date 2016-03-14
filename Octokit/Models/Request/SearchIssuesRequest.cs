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

        [Obsolete("this will be deprecated in a future version")]
        public SearchIssuesRequest(string term, string owner, string name)
            : this(term)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            Repos.Add(owner, name);
        }

        /// <summary>
        /// Optional Sort field. One of comments, created, updated or merged 
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
        /// Filters issues based on times when they were last merged
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-issues/#search-based-on-when-a-pull-request-was-merged
        /// </remarks>
        public DateRange Merged { get; set; }
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

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public RepositoryCollection Repos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override IReadOnlyList<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if (In != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "in:{0}",
                    string.Join(",", In.Select(i => i.ToParameter()))));
            }

            if (Type != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "type:{0}",
                    Type.ToParameter()));
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

            if (State.HasValue)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "state:{0}", State.Value.ToParameter()));
            }

            if (Labels != null)
            {
                parameters.AddRange(Labels.Select(label => string.Format(CultureInfo.InvariantCulture, "label:{0}", label)));
            }

            if (Language != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "language:{0}", Language));
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

                parameters.Add(
                    string.Join("+", Repos.Select(x => "repo:" + x)));
            }

            return new ReadOnlyCollection<string>(parameters);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Term: {0}", Term);
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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
