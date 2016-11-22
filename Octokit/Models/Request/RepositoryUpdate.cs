using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents updatable fields on a repository. Values that are null will not be sent in the request.
    /// Use string.empty if you want to clear a value.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryUpdate
    {
        [Obsolete("Please use the ctor RepositoryUpdate(string name) as Name is a required field")]
        public RepositoryUpdate()
        {

        }

        /// <summary>
        /// Creates an object that describes an update to a repository on GitHub.
        /// </summary>
        /// <param name="name">The name of the repository. This is the only required parameter.</param>
        public RepositoryUpdate(string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            Name = name;
        }

        /// <summary>
        /// Required. Gets or sets the repository name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Optional. Gets or sets the repository description. The default is null (do not update)
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Optional. Gets or sets the repository homepage url. The default is null (do not update).
        /// </summary>
        public string Homepage { get; set; }
        
        /// <summary>
        /// Gets or sets whether to make the repository private. The default is null (do not update).
        /// </summary>
        public bool? Private { get; set; }
        
        /// <summary>
        /// Gets or sets whether to enable issues for the repository. The default is null (do not update).
        /// </summary>
        public bool? HasIssues { get; set; }
        
        /// <summary>
        /// Optional. Gets or sets whether to enable the wiki for the repository. The default is null (do not update).
        /// </summary>
        public bool? HasWiki { get; set; }
        
        /// <summary>
        /// Optional. Gets or sets whether to enable downloads for the repository. The default is null (do not update).
        /// </summary>
        public bool? HasDownloads { get; set; }
        
        /// <summary>
        /// Optional. Gets or sets the default branch. The default is null (do not update).
        /// </summary>
        public string DefaultBranch { get; set; }
        
        /// <summary>
        /// Optional. Allows the "Rebase and Merge" method to be used.
        /// </summary>
        public bool? AllowRebaseMerge { get; set; }

        /// <summary>
        /// Optional. Allows the "Squash Merge" merge method to be used.
        /// </summary>
        public bool? AllowSquashMerge { get; set; }

        /// <summary>
        /// Optional. Allows the "Create a merge commit" merge method to be used.
        /// </summary>
        public bool? AllowMergeCommit { get; set; }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.CurrentCulture, "RepositoryUpdate: Name: {0}", Name); }
        }
    }
}
