using System.Diagnostics;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Searching Repositories
    /// http://developer.github.com/v3/search/#search-repositories
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchRepositoriesRequest
    {
        public SearchRepositoriesRequest(string term)
        {
            Term = term;
            Page = 1;
            PerPage = 100;
            Fork = ForkQualifier.ExcludeForks;
            Order = SortDirection.Descending;
        }

        /// <summary>
        /// The search terms. This can be any combination of the supported repository search parameters:
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// For https://help.github.com/articles/searching-repositories#sorting
        /// Optional Sort field. One of stars, forks, or updated. If not provided, results are sorted by best match.
        /// </summary>
        public RepoSearchSort? Sort { get; set; }

        /// <summary>
        /// Sort order one of asc or desc - the default is desc.
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

        private IEnumerable<InQualifier> _inQualifier;

        /// <summary>
        /// The in qualifier limits what fields are searched. With this qualifier you can restrict the search to just the repository name, description, README, or any combination of these. 
        /// Without the qualifier, only the name and description are searched.
        /// https://help.github.com/articles/searching-repositories#search-in
        /// </summary>
        public IEnumerable<InQualifier> In
        {
            get
            {
                return _inQualifier;
            }
            set
            {
                if (value != null && value.Any())
                    _inQualifier = value.Distinct().ToList();
            }
        }

        /// <summary>
        /// Filters repositories based on the number of forks, and/or whether forked repositories should be included in the results at all.
        /// https://help.github.com/articles/searching-repositories#forks
        /// </summary>
        public Range Forks { get; set; }

        /// <summary>
        /// Filters repositories based whether forked repositories should be included in the results at all.
        /// Defaults to ExcludeForks
        /// https://help.github.com/articles/searching-repositories#forks
        /// </summary>
        public ForkQualifier Fork { get; set; }

        /// <summary>
        /// The size qualifier finds repository's that match a certain size (in kilobytes).
        /// https://help.github.com/articles/searching-repositories#size
        /// </summary>
        public Range Size { get; set; }

        /// <summary>
        /// Searches repositories based on the language they’re written in.
        /// https://help.github.com/articles/searching-repositories#languages
        /// </summary>
        public Language? Language { get; set; }

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
        /// Filters repositories based on times of creation.
        /// https://help.github.com/articles/searching-repositories#created-and-last-updated
        /// </summary>
        public DateRange Created { get; set; }

        /// <summary>
        /// Filters repositories based on when they were last updated.
        /// https://help.github.com/articles/searching-repositories#created-and-last-updated
        /// </summary>
        public DateRange Updated { get; set; }

        public string MergeParameters()
        {
            var parameters = new List<string>();

            if (In != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "in:{0}", String.Join(",", In)));
            }

            if (Size != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "size:{0}", Size));
            }

            if (Forks != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "forks:{0}", Forks));
            }

            parameters.Add(String.Format(CultureInfo.InvariantCulture, "fork:{0}", Fork));

            if (Stars != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "stars:{0}", Stars));
            }

            if (Language != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "language:{0}", Language));
            }

            if (User.IsNotBlank())
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "user:{0}", User));
            }

            if (Created != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "created:{0}", Created));
            }

            if (Updated != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "pushed:{0}", Updated));
            }

            return String.Join("+", parameters);
        }

        /// <summary>
        /// get the params in the correct format...
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public IDictionary<string, string> Parameters
        {
            get
            {
                var d = new System.Collections.Generic.Dictionary<string, string>();
                d.Add("page", Page.ToString());
                d.Add("per_page", PerPage.ToString());
                d.Add("sort", Sort.ToString());
                d.Add("q", Term + " " + MergeParameters()); //add qualifiers onto the search term
                return d;
            }
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Term: {0} Sort: {1}", Term, Sort);
            }
        }
    }

    /// <summary>
    /// https://help.github.com/articles/searching-repositories#search-in
    /// The in qualifier limits what fields are searched. With this qualifier you can restrict the search to just the 
    /// repository name, description, README, or any combination of these.
    /// </summary>
    public enum InQualifier
    {
        Name,
        Description,
        Readme
    }

    /// <summary>
    /// Helper class in generating the range values for a qualifer e.g. In or Size qualifiers
    /// </summary>
    public class Range
    {
        private string query = string.Empty;

        /// <summary>
        /// Matches repositories that are <param name="size">size</param> MB exactly
        /// </summary>
        /// <param name="size"></param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public Range(int size)
        {
            query = size.ToString();
        }

        /// <summary>
        /// Matches repositories that are between <see cref="minSize"/> and <see cref="maxSize"/> KB
        /// </summary>
        /// <param name="minSize"></param>
        /// <param name="maxSize"></param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public Range(int minSize, int maxSize)
        {
            query = string.Format("{0}..{1}", minSize, maxSize);
        }

        /// <summary>
        /// Matches repositories with regards to the size <see cref="size"/> 
        /// We will use the <see cref="op"/> to see what operator will be applied to the size qualifier
        /// </summary>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public Range(int size, SearchQualifierOperator op)
        {
            switch (op)
            {
                case SearchQualifierOperator.GreaterThan:
                    query = string.Format(">{0}", size.ToString());
                    break;
                case SearchQualifierOperator.LessThan:
                    query = string.Format("<{0}", size.ToString());
                    break;
                case SearchQualifierOperator.LessThanOrEqualTo:
                    query = string.Format("<={0}", size.ToString());
                    break;
                case SearchQualifierOperator.GreaterThanOrEqualTo:
                    query = string.Format(">={0}", size.ToString());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Helper class that build a <see cref="Range"/> with a LessThan comparator used for filtering results
        /// </summary>
        public static Range LessThan(int size)
        {
            return new Range(size, SearchQualifierOperator.LessThan);
        }

        /// <summary>
        /// Helper class that build a <see cref="Range"/> with a LessThanOrEqual comparator used for filtering results
        /// </summary>
        public static Range LessThanOrEquals(int size)
        {
            return new Range(size, SearchQualifierOperator.LessThanOrEqualTo);
        }

        /// <summary>
        /// Helper class that build a <see cref="Range"/> with a GreaterThan comparator used for filtering results
        /// </summary>
        public static Range GreaterThan(int size)
        {
            return new Range(size, SearchQualifierOperator.GreaterThan);
        }

        /// <summary>
        /// Helper class that build a <see cref="Range"/> with a GreaterThanOrEqualTo comparator used for filtering results
        /// </summary>
        public static Range GreaterThanOrEquals(int size)
        {
            return new Range(size, SearchQualifierOperator.GreaterThanOrEqualTo);
        }

        public override string ToString()
        {
            return query;
        }
    }

    /// <summary>
    /// helper class in generating the date range values for the date qualifier e.g.
    /// https://help.github.com/articles/searching-repositories#created-and-last-updated
    /// </summary>
    public class DateRange
    {
        private string query = string.Empty;

        /// <summary>
        /// Matches repositories with regards to the date <see cref="date"/> 
        /// We will use the <see cref="op"/> to see what operator will be applied to the date qualifier
        /// </summary>
        /// <param name="date">The date</param>
        /// <param name="op">And its search operator</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public DateRange(DateTime date, SearchQualifierOperator op)
        {
            switch (op)
            {
                case SearchQualifierOperator.GreaterThan:
                    query = string.Format(">{0}", date.ToString("yyyy-MM-dd"));
                    break;
                case SearchQualifierOperator.LessThan:
                    query = string.Format("<{0}", date.ToString("yyyy-MM-dd"));
                    break;
                case SearchQualifierOperator.LessThanOrEqualTo:
                    query = string.Format("<={0}", date.ToString("yyyy-MM-dd"));
                    break;
                case SearchQualifierOperator.GreaterThanOrEqualTo:
                    query = string.Format(">={0}", date.ToString("yyyy-MM-dd"));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// helper method to create a LessThan Date Comparision
        /// e.g. &lt; 2011
        /// </summary>
        /// <param name="date">date to be used for comparision (times are ignored)</param>
        /// <returns><see cref="DateRange"/></returns>
        public static DateRange LessThan(DateTime date)
        {
            return new DateRange(date, SearchQualifierOperator.LessThan);
        }

        /// <summary>
        /// helper method to create a LessThanOrEqualTo Date Comparision
        /// e.g. &lt;= 2011
        /// </summary>
        /// <param name="date">date to be used for comparision (times are ignored)</param>
        /// <returns><see cref="DateRange"/></returns>
        public static DateRange LessThanOrEquals(DateTime date)
        {
            return new DateRange(date, SearchQualifierOperator.LessThanOrEqualTo);
        }

        /// <summary>
        /// helper method to create a GreaterThan Date Comparision
        /// e.g. > 2011
        /// </summary>
        /// <param name="date">date to be used for comparision (times are ignored)</param>
        /// <returns><see cref="DateRange"/></returns>
        public static DateRange GreaterThan(DateTime date)
        {
            return new DateRange(date, SearchQualifierOperator.GreaterThan);
        }

        /// <summary>
        /// helper method to create a GreaterThanOrEqualTo Date Comparision
        /// e.g. >= 2011
        /// </summary>
        /// <param name="date">date to be used for comparision (times are ignored)</param>
        /// <returns><see cref="DateRange"/></returns>
        public static DateRange GreaterThanOrEquals(DateTime date)
        {
            return new DateRange(date, SearchQualifierOperator.GreaterThanOrEqualTo);
        }

        public override string ToString()
        {
            return query;
        }
    }

    /// <summary>
    /// lanuages that can be searched on in github
    /// https://help.github.com/articles/searching-repositories#languages
    /// </summary>
    public enum Language
    {
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Abap")]
        Abap,
        [Parameter(Value = "ActionScript")]
        ActionScript,
        Ada,
        Apex,
        [Parameter(Value = "AppleScript")]
        AppleScript,
        Arc,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Arduino")]
        Arduino,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Conf")]
        [Parameter(Value = "ApacheConf")]
        ApacheConf,
        Asp,
        Assembly,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Augeas")]
        Augeas,
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HotKey")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HotKey")]
        [Parameter(Value = "AutoHotkey")]
        AutoHotKey,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awk")]
        Awk,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Batchfile")]
        Batchfile,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Befunge")]
        Befunge,
        [Parameter(Value = "BlitzMax")]
        BlitzMax,
        Boo,
        Bro,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C")]
        C,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "hs")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "hs")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "hs")]
        [Parameter(Value = "C2HS Haskell")]
        C2hsHaskell,
        Ceylon,
        Chuck,
        Clips,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Clojure")]
        Clojure,
        Cobol,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cmake")]
        Cmake,
        [Parameter(Value = "C-ObjDump")]
        CObjDump,
        [Parameter(Value = "CoffeeScript")]
        CoffeeScript,
        [Parameter(Value = "ColdFusion")]
        ColdFusion,
        CommonLisp,
        Coq,
        [Parameter(Value = "C++")]
        CPlusPlus,
        [Parameter(Value = "C#")]
        CSharp,
        Css,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cpp")]
        [Parameter(Value = "Cpp-ObjDump")]
        CppObjDump,
        Cucumber,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cython")]
        Cython,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D")]
        D,
        [Parameter(Value = "D-ObjDump")]
        DObjDump,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Darcs")]
        [Parameter(Value = "DarcsPatch")]
        DarcsPatch,
        Dart,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dcpu")]
        [Parameter(Value = "DCPU-16 ASM")]
        Dcpu16Asm,
        Dot,
        Dylan,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ec")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ec")]
        Ec,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ecere")]
        [Parameter(Value = "Ecere Projects")]
        EcereProjects,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ecl")]
        Ecl,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Edn")]
        Edn,
        Eiffel,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Elixer")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Elixer")]
        Elixer,
        Elm,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Emacs")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Emacs")]
        EmacsLisp,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Erlang")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Erlang")]
        Erlang,
        [Parameter(Value = "F#")]
        FSharp,
        Factor,
        Fancy,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fantom")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fantom")]
        Fantom,
        Fish,
        Forth,
        Fortran,
        Gas,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Genshi")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Genshi")]
        Genshi,
        [Parameter(Value = "Gentoo Build")]
        GentooBuild,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eclass")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eclass")]
        [Parameter(Value = "Gentoo Eclass")]
        GentooEclass,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gettext")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gettext")]
        [Parameter(Value = "Gettext Catalog")]
        GettextCatalog,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Glsl")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Glsl")]
        Glsl,
        Go,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gosu")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gosu")]
        Gosu,
        Groff,
        Groovy,
        [Parameter(Value = "Groovy Server Pages")]
        GroovyServerPages,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Haml")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Haml")]
        Haml,
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HandleBars")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HandleBars")]
        [Parameter(Value = "HandleBars")]
        HandleBars,
        Haskell,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Haxe")]
        Haxe,
        Http,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ini")]
        Ini,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Io")]
        Io,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ioke")]
        Ioke,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Irc")]
        [Parameter(Value = "IRC log")]
        IrcLog,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "J")]
        J,
        Java,
        [Parameter(Value = "Java Server Pages")]
        JavaServerPages,
        JavaScript,
        Json,
        Julia,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Kotlin")]
        Kotlin,
        Lasso,
        Less,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Lfe")]
        Lfe,
        [Parameter(Value = "LillyPond")]
        LillyPond,
        [Parameter(Value = "Literate CoffeeScript")]
        LiterateCoffeeScript,
        [Parameter(Value = "Literate Haskell")]
        LiterateHaskell,
        [Parameter(Value = "LiveScript")]
        LiveScript,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Llvm")]
        Llvm,
        Logos,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Logtalk")]
        Logtalk,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Lua")]
        Lua,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M")]
        M,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Makefile")]
        Makefile,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mako")]
        Mako,
        Markdown,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Matlab")]
        Matlab,
        Max,
        [Parameter(Value = "MiniD")]
        MiniD,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mirah")]
        Mirah,
        Monkey,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Moocode")]
        Moocode,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Moonscript")]
        Moonscript,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mupad")]
        Mupad,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Myghty")]
        Myghty,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nemerle")]
        Nemerle,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nginx")]
        Nginx,
        Nimrod,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nsis")]
        Nsis,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Nu")]
        Nu,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
        [Parameter(Value = "NumPY")]
        NumPY,
        [Parameter(Value = "ObjDump")]
        ObjDump,
        ObjectiveC,
        ObjectiveJ,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Caml")]
        [Parameter(Value = "OCaml")]
        OCaml,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Omgrofl")]
        Omgrofl,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ooc")]
        Ooc,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Opa")]
        Opa,
        [Parameter(Value = "OpenCL")]
        OpenCL,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Abl")]
        [Parameter(Value = "OpenEdge ABL")]
        OpenEdgeAbl,
        Parrot,
        [Parameter(Value = "Parrot Assembly")]
        ParrotAssembly,
        [Parameter(Value = "Parrot Internal Representation")]
        ParrotInternalRepresentation,
        Pascal,
        Perl,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Php")]
        Php,
        Pike,
        [Parameter(Value = "PogoScript")]
        PogoScript,
        [Parameter(Value = "PowerShell")]
        PowerShell,
        Processing,
        Prolog,
        Puppet,
        [Parameter(Value = "Pure Data")]
        PureData,
        Python,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Traceback")]
        [Parameter(Value = "Python traceback")]
        PythonTraceback,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R")]
        R,
        Racket,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ragel")]
        [Parameter(Value = "Ragel in Ruby Host")]
        RagelInRubyHost,
        [Parameter(Value = "Raw token data")]
        RawTokenData,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rebol")]
        Rebol,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Redcode")]
        Redcode,
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ReStructured")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Re")]
        [Parameter(Value = "reStructuredText")]
        ReStructuredText,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rhtml")]
        Rhtml,
        Rouge,
        Ruby,
        Rust,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scala")]
        Scala,
        Scheme,
        Sage,
        Sass,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scilab")]
        Scilab,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scss")]
        Scss,
        Self,
        Shell,
        Slash,
        Smalltalk,
        Smarty,
        Squirrel,
        [Parameter(Value = "Standard ML")]
        StandardML,
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SuperCollider")]
        [Parameter(Value = "SuperCollider")]
        SuperCollider,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tcl")]
        Tcl,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tcsh")]
        Tcsh,
        Tea,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Te")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Te")]
        [Parameter(Value = "TeX")]
        TeX,
        Textile,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toml")]
        Toml,
        Turing,
        Twig,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Txl")]
        Txl,
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TypeScript")]
        [Parameter(Value = "TypeScript")]
        TypeScript,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Paralel")]
        [Parameter(Value = "Unified Paralel C")]
        UnifiedParalelC,
        Unknown,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Vala")]
        Vala,
        Verilog,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Vhdl")]
        Vhdl,
        [Parameter(Value = "VimL")]
        VimL,
        VisualBasic,
        Volt,
        Wisp,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Xc")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xc")]
        Xc,
        Xml,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Proc")]
        [Parameter(Value = "XProc")]
        XProc,
        [Parameter(Value = "XQuery")]
        XQuery,
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Xs")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xs")]
        Xs,
        Xslt,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xtend")]
        Xtend,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Yaml")]
        Yaml
    }

    /// <summary>
    /// sorting repositories by any of below
    /// https://help.github.com/articles/searching-repositories#sorting
    /// </summary>
    public enum RepoSearchSort
    {
        /// <summary>
        /// search by number of stars
        /// </summary>
        Stars,
        /// <summary>
        /// search by number of forks
        /// </summary>
        Forks,
        /// <summary>
        /// search by last updated
        /// </summary>
        Updated
    }

    /// <summary>
    /// https://help.github.com/articles/searching-repositories#forks
    /// Specifying whether forked repositories should be included in results or not.
    /// </summary>
    public enum ForkQualifier
    {
        /// <summary>
        /// only search for forked repos
        /// </summary>
        OnlyForks,
        /// <summary>
        /// include forked repos into the search
        /// </summary>
        IncludeForks,
        /// <summary>
        /// forks are not included in the search (default behaviour)
        /// </summary>
        ExcludeForks
    }
}