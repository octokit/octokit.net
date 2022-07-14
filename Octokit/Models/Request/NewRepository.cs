using Octokit.Internal;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new repository to create via the <see cref="IRepositoriesClient.Create(NewRepository)"/> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepository
    {
        /// <summary>
        /// Creates an object that describes the repository to create on GitHub.
        /// </summary>
        /// <param name="name">The name of the repository. This is the only required parameter.</param>
        public NewRepository(string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// Optional. Gets or sets whether to create an initial commit with empty README. The default is false.
        /// </summary>
        public bool? AutoInit { get; set; }

        /// <summary>
        /// Required. Gets or sets the new repository's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether to enable downloads for the new repository. The default is true.
        /// </summary>
        public bool? HasDownloads { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether to enable issues for the new repository. The default is true.
        /// </summary>
        public bool? HasIssues { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether to enable projects for the new repository. The default is true.
        /// </summary>
        public bool? HasProjects { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether to enable the wiki for the new repository. The default is true.
        /// </summary>
        public bool? HasWiki { get; set; }

        /// <summary>
        /// Either true to make this repo available as a template repository or false to prevent it. Default: false.
        /// </summary>
        public bool? IsTemplate { get; set; }

        /// <summary>
        /// Optional. Gets or sets the new repository's optional website.
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        /// Optional. Gets or sets the desired language's or platform's .gitignore template to apply. Use the name of the template without the extension; "Haskell", for example. Ignored if <see cref="AutoInit"/> is null or false.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gitignore", Justification = "It needs to be this way for proper serialization.")]
        public string GitignoreTemplate { get; set; }

        /// <summary>
        /// Optional. Gets or sets the desired LICENSE template to apply. Use the name of the template without
        /// the extension. For example, “mit” or “mozilla”.
        /// </summary>
        /// <remarks>
        /// The list of license templates are here: https://github.com/github/choosealicense.com/tree/gh-pages/_licenses
        /// Just omit the ".txt" file extension for the template name.
        /// </remarks>
        public string LicenseTemplate { get; set; }

        /// <summary>
        /// Required. Gets or sets the new repository's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Optional. Gets or sets whether the new repository is private; the default is false.
        /// </summary>
        public bool? Private { get; set; }

        /// <summary>
        /// Optional. Gets or sets the Id of the team to grant access to this repository. This is only valid when creating a repository for an organization.
        /// </summary>
        public int? TeamId { get; set; }

        public bool? DeleteBranchOnMerge { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the new repository is public, private, or internal. A value provided here overrides any value set in the existing private field.
        /// </summary>
        public RepositoryVisibility? Visibility { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the new repository allows rebase merges.
        /// </summary>
        public bool? AllowRebaseMerge { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the new repository allows squash merges.
        /// </summary>
        public bool? AllowSquashMerge { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the new repository allows merge commits.
        /// </summary>
        public bool? AllowMergeCommit { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the new repository allows auto merge.
        /// </summary>
        public bool? AllowAutoMerge { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the squash pr title is used as default when using Squash Merge. Default is false. Cannot currently be tested as it isn't returned in the GET method.
        /// </summary>
        public bool? UseSquashPrTitleAsDefault { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0} Description: {1}", Name, Description);
            }
        }
    }

    /// <summary>
    /// The properties that repositories can be visible by.
    /// </summary>
    public enum RepositoryVisibility
    {
        /// <summary>
        /// Sets repository visibility to public
        /// </summary>
        [Parameter(Value = "public")]
        Public,

        /// <summary>
        /// Sets repository visibility to private
        /// </summary>
        [Parameter(Value = "private")]
        Private,

        /// <summary>
        /// Sets repository visibility to internal
        /// </summary>
        [Parameter(Value = "internal")]
        Internal,
    }
}
