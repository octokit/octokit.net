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
    /// Searching Code/Files
    /// http://developer.github.com/v3/search/#search-code
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCodeRequest : BaseSearchRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCodeRequest"/> class.
        /// </summary>
        /// <param name="term">The search term.</param>
        public SearchCodeRequest(string term) : base(term)
        {
            Repos = new RepositoryCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCodeRequest"/> class.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="name">The name.</param>
        public SearchCodeRequest(string term, string owner, string name)
            : this(term)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            Repos.Add(owner, name);
        }

        /// <summary>
        /// Optional Sort field. Can only be indexed, which indicates how recently 
        /// a file has been indexed by the GitHub search infrastructure. 
        /// If not provided, results are sorted by best match.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/search/#search-code
        /// </remarks>
        public CodeSearchSort? SortField { get; set; }
        public override string Sort
        {
            get { return SortField.ToParameter(); }
        }

        /// <summary>
        /// Qualifies which fields are searched. With this qualifier you can restrict 
        /// the search to just the file contents, the file path, or both.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code#search-in
        /// </remarks>
        private IEnumerable<CodeInQualifier> _inQualifier;
        public IEnumerable<CodeInQualifier> In
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
        /// Searches code based on the language it’s written in.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code#language
        /// </remarks>
        public Language? Language { get; set; }

        /// <summary>
        /// Specifies that code from forked repositories should be searched. 
        /// Repository forks will not be searchable unless the fork has more 
        /// stars than the parent repository.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code#forks
        /// </remarks>
        public bool? Forks { get; set; }

        /// <summary>
        /// Finds files that match a certain size (in bytes).
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code#size
        /// </remarks>
        public Range Size { get; set; }

        /// <summary>
        /// Specifies the path that the resulting file must be at.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code#path
        /// </remarks>
        public string Path { get; set; }

        /// <summary>
        /// Matches files with a certain extension.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code#extension
        /// </remarks>
        public string Extension { get; set; }

        /// <summary>
        /// Matches specific file names
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code/#search-by-filename
        /// </remarks>
        public string FileName { get; set; }

        /// <summary>
        /// Limits searches to a specific user.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code#users-organizations-and-repositories
        /// </remarks>
        public string User { get; set; }

        /// <summary>
        /// Limits searches to a specific repository.
        /// </summary>
        /// <remarks>
        /// https://help.github.com/articles/searching-code#users-organizations-and-repositories
        /// </remarks>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public RepositoryCollection Repos { get; set; }

        [SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public override IReadOnlyList<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if (In != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "in:{0}",
                    string.Join(",", In.Select(i => i.ToParameter()))));
            }

            if (Language != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "language:{0}", Language.ToParameter()));
            }

            if (Forks != null)
            {
                // API is expecting 'true', bool.ToString() returns 'True', if there is a better way,
                // please, oh please let me know...
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "fork:{0}", Forks.Value.ToString().ToLower()));
            }

            if (Size != null)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "size:{0}", Size));
            }

            if (Path.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "path:{0}", Path));
            }

            if (Extension.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "extension:{0}", Extension));
            }

            if (FileName.IsNotBlank())
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "filename:{0}", FileName));
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
                return string.Format(CultureInfo.InvariantCulture, "Term: {0} Sort: {1}", Term, Sort);
            }
        }
    }

    public enum CodeSearchSort
    {
        [Parameter(Value = "indexed")]
        Indexed
    }

    public enum CodeInQualifier
    {
        [Parameter(Value = "file")]
        File,
        [Parameter(Value = "path")]
        Path
    }
}
