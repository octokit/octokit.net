using System;
using System.Collections.Generic;
namespace Octokit
{
    /// <summary>
    /// Searching Repositories
    /// </summary>
    public class RepositoriesRequest
    {
        public RepositoriesRequest(string term)
        {
            Term = term;
            Page = 1;
            PerPage = 100;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public RepositoriesRequest(string term, Range size = null, Range stars = null, Range forks = null)
        {
            Term = term;
            Page = 1;
            PerPage = 100;
            Size = size;
            Stars = stars;
        }

        /// <summary>
        /// The search terms. This can be any combination of the supported repository search parameters:
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// For http://developer.github.com/v3/search/#search-repositories
        /// Optional Sort field. One of stars, forks, or updated. If not provided, results are sorted by best match.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Optional Sort order if sort parameter is provided. One of asc or desc; the default is desc.
        /// </summary>
        public SortDirection? Order { get; set; }

        /// <summary>
        /// Page of paginated results
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The in qualifier limits what fields are searched. With this qualifier you can restrict the search to just the repository name, description, README, or any combination of these. 
        /// Without the qualifier, only the name and description are searched.
        /// https://help.github.com/articles/searching-repositories#search-in
        /// </summary>
        public InQualifier In { get; set; }

        /// <summary>
        /// Filters repositories based on the number of forks, and/or whether forked repositories should be included in the results at all.
        /// https://help.github.com/articles/searching-repositories#forks
        /// </summary>
        public Range Forks { get; set; }

        /// <summary>
        /// Filters repositories based whether forked repositories should be included in the results at all.
        /// https://help.github.com/articles/searching-repositories#forks
        /// </summary>
        public bool? Fork { get; set; }

        /// <summary>
        /// The size qualifier finds repository's that match a certain size (in kilobytes).
        /// https://help.github.com/articles/searching-repositories#size
        /// </summary>
        public Range Size { get; set; }

        /// <summary>
        /// Searches repositories based on the language they’re written in.
        /// https://help.github.com/articles/searching-repositories#languages
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Searches repositories based on the number of stars.
        /// https://help.github.com/articles/searching-repositories#stars
        /// </summary>
        public Range Stars { get; set; }

        /// <summary>
        /// Limits searches to a specific user or repository.
        /// https://help.github.com/articles/searching-repositories#users-organizations-and-repositories
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Filters repositories based on times of creation, or when they were last updated.
        /// https://help.github.com/articles/searching-repositories#created-and-last-updated
        /// </summary>
        public string Created { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public string MergeParameters()
        {
            var parameters = new List<string>();

            if (In != null)
            {
                parameters.Add(String.Format("in:{0}", In));
            }

            if (Size != null)
            {
                parameters.Add(String.Format("size:{0}", Size));
            }

            if (Forks != null)
            {
                parameters.Add(String.Format("forks:{0}", Forks));
            }


            if (Stars != null)
            {
                parameters.Add(String.Format("stars:{0}", Forks));
            }

            return String.Join("+", parameters);
        }

        /// <summary>
        /// get the params in the correct format...
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public System.Collections.Generic.IDictionary<string, string> Parameters
        {
            get
            {
                var d = new System.Collections.Generic.Dictionary<string, string>();
                d.Add("page", Page.ToString());
                d.Add("per_page", PerPage.ToString());
                d.Add("q", Term + " " + MergeParameters()); //add qualifiers onto the search term
                return d;
            }
        }
    }

    public class InQualifier
    {
        private string query = string.Empty;

        public InQualifier()
        {

        }

        public override string ToString()
        {
            return query;
        }
    }

    /// <summary>
    /// Helper method in generating the range values for a qualifer e.g. In or Size qualifiers
    /// </summary>
    public class Range
    {
        private string query = string.Empty;

        /// <summary>
        /// Matches repositories that are <param name="size">size</param> MB exactly
        /// </summary>
        /// <param name="size"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public Range(int size)
        {
            query = size.ToString();
        }

        /// <summary>
        /// Matches repositories that are between <see cref="minSize"/> and <see cref="maxSize"/> KB
        /// </summary>
        /// <param name="size"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public Range(int minSize, int maxSize)
        {
            query = string.Format("{0}..{1}", minSize.ToString(), maxSize.ToString());
        }

        /// <summary>
        /// Matches repositories with regards to the size <see cref="size"/> 
        /// We will use the <see cref="op"/> to see what operator will be applied to the size qualifier
        /// </summary>
        /// <param name="size"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public Range(int size, QualifierOperator op)
        {
            switch (op)
            {
                case QualifierOperator.GreaterThan:
                    query = string.Format(">{0}", size.ToString());
                    break;
                case QualifierOperator.LessThan:
                    query = string.Format("<{0}", size.ToString());
                    break;
                case QualifierOperator.LessOrEqualTo:
                    query = string.Format("<={0}", size.ToString());
                    break;
                case QualifierOperator.GreaterOrEqualTo:
                    query = string.Format(">={0}", size.ToString());
                    break;
                default:
                    break;
            }
        }

        public static Range LessThan(int size)
        {
            return new Range(size, QualifierOperator.LessThan);
        }

        public static Range LessThanOrEquals(int size)
        {
            return new Range(size, QualifierOperator.LessOrEqualTo);
        }

        public static Range GreaterThan(int size)
        {
            return new Range(size, QualifierOperator.GreaterThan);
        }

        public static Range GreaterThanOrEquals(int size)
        {
            return new Range(size, QualifierOperator.GreaterOrEqualTo);
        }

        public override string ToString()
        {
            return query;
        }
    }

    public enum QualifierOperator
    {
        GreaterThan, // >
        LessThan, // <
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LessOr")]
        LessOrEqualTo, // <=
        GreaterOrEqualTo// >=
    }

    /// <summary>
    /// Searching Users
    /// </summary>
    public class UsersRequest
    {
        public UsersRequest(string term)
        {
            Term = term;
            Page = 1;
            PerPage = 100;
        }

        /// <summary>
        /// The search terms. This can be any combination of the supported repository search parameters:
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// For http://developer.github.com/v3/search/#search-users
        /// Optional Sort field. One of followers, repositories, or joined. If not provided, results are sorted by best match.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Optional Sort order if sort parameter is provided. One of asc or desc; the default is desc.
        /// </summary>
        public SortDirection? Order { get; set; }

        /// <summary>
        /// Page of paginated results
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// get the params in the correct format...
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public System.Collections.Generic.IDictionary<string, string> Parameters
        {
            get
            {
                var d = new System.Collections.Generic.Dictionary<string, string>();
                d.Add("q", Term);
                d.Add("page", Page.ToString());
                d.Add("per_page ", PerPage.ToString());

                if (Sort.IsNotBlank()) //only add if not blank
                    d.Add("sort", Sort);

                if (Order.HasValue)
                    d.Add("order", Order.Value.ToString());

                return d;
            }
        }
    }

    /// <summary>
    /// Searching Code/Files
    /// </summary>
    public class CodeRequest
    {
        public CodeRequest(string term)
        {
            Term = term;
            Page = 1;
            PerPage = 100;
        }

        /// <summary>
        /// The search terms. This can be any combination of the supported repository search parameters:
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// For http://developer.github.com/v3/search/#search-code
        /// Optional Sort field. Can only be indexed, which indicates how recently a file has been indexed by the GitHub search infrastructure. If not provided, results are sorted by best match.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Optional Sort order if sort parameter is provided. One of asc or desc; the default is desc.
        /// </summary>
        public SortDirection? Order { get; set; }

        /// <summary>
        /// Page of paginated results
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// get the params in the correct format...
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public System.Collections.Generic.IDictionary<string, string> Parameters
        {
            get
            {
                var d = new System.Collections.Generic.Dictionary<string, string>();
                d.Add("q", Term);
                d.Add("page", Page.ToString());
                d.Add("per_page ", PerPage.ToString());

                if (Sort.IsNotBlank()) //only add if not blank
                    d.Add("sort", Sort);

                if (Order.HasValue)
                    d.Add("order", Order.Value.ToString());

                return d;
            }
        }
    }

    /// <summary>
    /// Searching Issues
    /// </summary>
    public class IssuesRequest
    {
        public IssuesRequest(string term)
        {
            Term = term;
            Page = 1;
            PerPage = 100;
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
        public string Sort { get; set; }

        /// <summary>
        /// Optional Sort order if sort parameter is provided. One of asc or desc; the default is desc.
        /// </summary>
        public SortDirection? Order { get; set; }

        /// <summary>
        /// Page of paginated results
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// get the params in the correct format...
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public System.Collections.Generic.IDictionary<string, string> Parameters
        {
            get
            {
                var d = new System.Collections.Generic.Dictionary<string, string>();
                d.Add("q", Term);
                d.Add("page", Page.ToString());
                d.Add("per_page ", PerPage.ToString());

                if (Sort.IsNotBlank()) //only add if not blank
                    d.Add("sort", Sort);

                if (Order.HasValue)
                    d.Add("order", Order.Value.ToString());

                return d;
            }
        }
    }
}