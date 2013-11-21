using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Searching Repositories
    /// </summary>
    public class RepositoriesRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public RepositoriesRequest(string term, Range size = null, Range stars = null, Range forks = null, Language? language = null, 
            IEnumerable<InQualifier> inQualifiers = null, string user = null, RepoSearchSort? sort = null)
        {
            Term = term;
            Page = 1;
            PerPage = 100;
            Size = size;
            Stars = stars;
            Forks = forks;
            Language = language;
            User = user;
            Sort = sort;

            if (inQualifiers != null && inQualifiers.Count() > 0)
                In = inQualifiers.Distinct().ToList();
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
        public RepoSearchSort? Sort { get; set; }

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
        public IEnumerable<InQualifier> In { get; set; }

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
                parameters.Add(String.Format("in:{0}", String.Join(",", In)));
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
                parameters.Add(String.Format("stars:{0}", Stars));
            }

            if (Language != null)
            {
                parameters.Add(String.Format("language:{0}", Language.Value.ToString()));
            }

            if (User.IsNotBlank())
            {
                parameters.Add(String.Format("user:{0}", User));
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
                d.Add("sort", Sort.ToString());
                d.Add("q", Term + " " + MergeParameters()); //add qualifiers onto the search term
                return d;
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

    public enum Language
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Abap")]
        Abap,
        [Parameter(Value = "ActionScript")]
        ActionScript,
        Ada,
        Apex,
        [Parameter(Value = "AppleScript")]
        AppleScript,
        Arc,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Arduino")]
        Arduino,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Conf")]
        [Parameter(Value = "ApacheConf")]
        ApacheConf,
        Asp,
        Assembly,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Augeas")]
        Augeas,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HotKey")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HotKey")]
        [Parameter(Value = "AutoHotkey")]
        AutoHotKey,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awk")]
        Awk,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Batchfile")]
        Batchfile,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Befunge")]
        Befunge,
        [Parameter(Value = "BlitzMax")]
        BlitzMax,
        Boo,
        Bro,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C")]
        C,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "hs")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "hs")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "hs")]
        [Parameter(Value = "C2HS Haskell")]
        C2hsHaskell,
        Ceylon,
        Chuck,
        Clips,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Clojure")]
        Clojure,
        Cobol,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cmake")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cpp")]
        [Parameter(Value = "Cpp-ObjDump")]
        CppObjDump,
        Cucumber,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cython")]
        Cython,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D")]
        D,
        [Parameter(Value = "D-ObjDump")]
        DObjDump,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Darcs")]
        [Parameter(Value = "DarcsPatch")]
        DarcsPatch,
        Dart,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dcpu")]
        [Parameter(Value = "DCPU-16 ASM")]
        Dcpu16Asm,
        Dot,
        Dylan,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ec")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ec")]
        Ec,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ecere")]
        [Parameter(Value = "Ecere Projects")]
        EcereProjects,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ecl")]
        Ecl,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Edn")]
        Edn,
        Eiffel,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Elixer")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Elixer")]
        Elixer,
        Elm,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Emacs")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Emacs")]
        EmacsLisp,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Erlang")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Erlang")]
        Erlang,
        [Parameter(Value = "F#")]
        FSharp,
        Factor,
        Fancy,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fantom")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fantom")]
        Fantom,
        Fish,
        Forth,
        Fortran,
        Gas,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Genshi")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Genshi")]
        Genshi,
        [Parameter(Value = "Gentoo Build")]
        GentooBuild,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eclass")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eclass")]
        [Parameter(Value = "Gentoo Eclass")]
        GentooEclass,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gettext")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gettext")]
        [Parameter(Value = "Gettext Catalog")]
        GettextCatalog,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Glsl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Glsl")]
        Glsl,
        Go,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gosu")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gosu")]
        Gosu,
        Groff,
        Groovy,
        [Parameter(Value = "Groovy Server Pages")]
        GroovyServerPages,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Haml")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Haml")]
        Haml,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HandleBars")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HandleBars")]
        [Parameter(Value = "HandleBars")]
        HandleBars,
        Haskell,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Haxe")]
        Haxe,
        Http,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ini")]
        Ini,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Io")]
        Io,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ioke")]
        Ioke,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Irc")]
        [Parameter(Value = "IRC log")]
        IrcLog,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "J")]
        J,
        Java,
        [Parameter(Value = "Java Server Pages")]
        JavaServerPages,
        JavaScript,
        Json,
        Julia,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Kotlin")]
        Kotlin,
        Lasso,
        Less,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Lfe")]
        Lfe,
        [Parameter(Value = "LillyPond")]
        LillyPond,
        [Parameter(Value = "Literate CoffeeScript")]
        LiterateCoffeeScript,
        [Parameter(Value = "Literate Haskell")]
        LiterateHaskell,
        [Parameter(Value = "LiveScript")]
        LiveScript,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Llvm")]
        Llvm,
        Logos,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Logtalk")]
        Logtalk,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Lua")]
        Lua,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M")]
        M,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Makefile")]
        Makefile,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mako")]
        Mako,
        Markdown,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Matlab")]
        Matlab,
        Max,
        [Parameter(Value = "MiniD")]
        MiniD,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mirah")]
        Mirah,
        Monkey,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Moocode")]
        Moocode,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Moonscript")]
        Moonscript,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mupad")]
        Mupad,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Myghty")]
        Myghty,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nemerle")]
        Nemerle,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nginx")]
        Nginx,
        Nimrod,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nsis")]
        Nsis,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Nu")]
        Nu,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
        [Parameter(Value = "NumPY")]
        NumPY,
        [Parameter(Value = "ObjDump")]
        ObjDump,
        ObjectiveC,
        ObjectiveJ,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Caml")]
        [Parameter(Value = "OCaml")]
        OCaml,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Omgrofl")]
        Omgrofl,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ooc")]
        Ooc,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Opa")]
        Opa,
        [Parameter(Value = "OpenCL")]
        OpenCL,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Abl")]
        [Parameter(Value = "OpenEdge ABL")]
        OpenEdgeAbl,
        Parrot,
        [Parameter(Value = "Parrot Assembly")]
        ParrotAssembly,
        [Parameter(Value = "Parrot Internal Representation")]
        ParrotInternalRepresentation,
        Pascal,
        Perl,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Php")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Traceback")]
        [Parameter(Value = "Python traceback")]
        PythonTraceback,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R")]
        R,
        Racket,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ragel")]
        [Parameter(Value = "Ragel in Ruby Host")]
        RagelInRubyHost,
        [Parameter(Value = "Raw token data")]
        RawTokenData,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rebol")]
        Rebol,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Redcode")]
        Redcode,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ReStructured")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Re")]
        [Parameter(Value = "reStructuredText")]
        ReStructuredText,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rhtml")]
        Rhtml,
        Rouge,
        Ruby,
        Rust,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scala")]
        Scala,
        Scheme,
        Sage,
        Sass,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scilab")]
        Scilab,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scss")]
        Scss,
        Self,
        Shell,
        Slash,
        Smalltalk,
        Smarty,
        Squirrel,
        [Parameter(Value = "Standard ML")]
        StandardML,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SuperCollider")]
        [Parameter(Value = "SuperCollider")]
        SuperCollider,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tcl")]
        Tcl,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tcsh")]
        Tcsh,
        Tea,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Te")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Te")]
        [Parameter(Value = "TeX")]
        TeX,
        Textile,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toml")]
        Toml,
        Turing,
        Twig,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Txl")]
        Txl,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TypeScript")]
        [Parameter(Value = "TypeScript")]
        TypeScript,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Paralel")]
        [Parameter(Value = "Unified Paralel C")]
        UnifiedParalelC,
        Unknown,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Vala")]
        Vala,
        Verilog,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Vhdl")]
        Vhdl,
        [Parameter(Value = "VimL")]
        VimL,
        VisualBasic,
        Volt,
        Wisp,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Xc")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xc")]
        Xc,
        Xml,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Proc")]
        [Parameter(Value = "XProc")]
        XProc,
        [Parameter(Value = "XQuery")]
        XQuery,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Xs")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xs")]
        Xs,
        Xslt,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xtend")]
        Xtend,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Yaml")]
        Yaml
    }

    public enum QualifierOperator
    {
        GreaterThan, // >
        LessThan, // <
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LessOr")]
        LessOrEqualTo, // <=
        GreaterOrEqualTo// >=
    }

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