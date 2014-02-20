using System;
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
        /// Optional. Gets or sets whether to create an initial commit with empty README. The default is false.
        /// </summary>
        public bool? AutoInit { get; set; }

        /// <summary>
        /// Required. Gets or sets the new repository's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>s
        /// Optional. Gets or sets whether to the enable downloads for the new repository. The default is true.
        /// </summary>
        public bool? HasDownloads { get; set; }

        /// <summary>s
        /// Optional. Gets or sets whether to the enable issues for the new repository. The default is true.
        /// </summary>
        public bool? HasIssues { get; set; }

        /// <summary>s
        /// Optional. Gets or sets whether to the enable the wiki for the new repository. The default is true.
        /// </summary>
        public bool? HasWiki { get; set; }

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
        /// Required. Gets or sets the new repository's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the new repository is private; the default is false.
        /// </summary>
        public bool? Private { get; set; }

        /// <summary>
        /// Optional. Gets or sets the ID of the team to grant access to this repository. This is only valid when creating a repository for an organization.
        /// </summary>
        public int? TeamId { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0} Description: {1}", Name, Description);
            }
        }
    }
}
