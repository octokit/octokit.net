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
        public SearchCodeRequest(string term) : base(term) { }

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
        public string Repo { get; set; }

        [SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public override IReadOnlyCollection<string> MergedQualifiers()
        {
            var parameters = new List<string>();

            if (In != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "in:{0}",
                    String.Join(",", In.Select(i => i.ToParameter()))));
            }

            if (Language != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "language:{0}", Language.ToParameter()));
            }

            if (Forks != null)
            {
                // API is expecting 'true', bool.ToString() returns 'True', if there is a better way,
                // please, oh please let me know...
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "fork:{0}", Forks.Value.ToString().ToLower()));
            }

            if (Size != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "size:{0}", Size));
            }

            if (Path.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "path:{0}", Path));
            }

            if (Extension.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "extension:{0}", Extension));
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
                return String.Format(CultureInfo.InvariantCulture, "Term: {0} Sort: {1}", Term, Sort);
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
