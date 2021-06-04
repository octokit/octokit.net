using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new repository to create via the <see cref="IRepositoriesClient.Generate"/> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepositoryFromTemplate
    {
        /// <summary>
        /// Creates an object that describes the repository to create on GitHub.
        /// </summary>
        /// <param name="name">The name of the repository. This is the only required parameter.</param>
        public NewRepositoryFromTemplate(string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// Optional. The organization or person who will own the new repository.
        /// To create a new repository in an organization, the authenticated user must be a member of the specified organization.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Required. The name of the new repository.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional. A short description of the new repository.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Optional. Either true to create a new private repository or false to create a new public one. Default: false
        /// </summary>
        public bool Private { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Name: {0} Description: {1}", Name, Description);
    }
}
