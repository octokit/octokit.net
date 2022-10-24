using Octokit.Internal;
using System;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Represents updatable fields on a repository. Values that are null will not be sent in the request.
    /// Use string.empty if you want to clear a value.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryUpdate
    {
        /// <summary>
        /// Creates an object that describes an update to a repository on GitHub.
        /// </summary>
        public RepositoryUpdate() { }

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
        /// Optional. Gets or sets whether the new repository is public, private, or internal. A value provided here overrides any value set in the existing private field.
        /// </summary>
        public RepositoryVisibility? Visibility { get; set; }

        // Yet to be implemented
        //public object SecurityAndAnalysis { get; set; }

        /// <summary>
        /// Gets or sets whether to enable issues for the repository. The default is null (do not update). The default when created is true.
        /// </summary>
        public bool? HasIssues { get; set; }

        /// <summary>
        /// Gets or sets whether to enable projects for the repository. The default is null (do not update). The default when created is true.
        /// </summary>
        public bool? HasProjects { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether to enable the wiki for the repository. The default is null (do not update). The default when created is true.
        /// </summary>
        public bool? HasWiki { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether to enable downloads for the repository. The default is null (do not update). No longer appears on the documentation but still works.
        /// </summary>
        public bool? HasDownloads { get; set; }

        /// <summary>
        /// Optional. Gets or sets whether the repository is a template. The default is null (do not update). The default when created is false.
        /// </summary>
        public bool? IsTemplate { get; set; }

        /// <summary>
        /// Optional. Gets or sets the default branch. The default is null (do not update).
        /// </summary>
        public string DefaultBranch { get; set; }

        /// <summary>
        /// Optional. Allows the "Squash Merge" merge method to be used. The default is null (do not update). The default when created is true.
        /// </summary>
        public bool? AllowSquashMerge { get; set; }

        /// <summary>
        /// Optional. Allows the "Create a merge commit" merge method to be used. The default is null (do not update). The default when created is true.
        /// </summary>
        public bool? AllowMergeCommit { get; set; }

        /// <summary>
        /// Optional. Allows the "Rebase and Merge" method to be used. The default is null (do not update). The default when created is true.
        /// </summary>
        public bool? AllowRebaseMerge { get; set; }

        /// <summary>
        /// Optional. Allows the auto merge feature to be used. The default is null (do not update). The default when created is false.
        /// </summary>
        public bool? AllowAutoMerge { get; set; }

        /// <summary>
        /// Optional. Automatically delete branches on PR merge. The default is null (do not update). The default when created is false.
        /// </summary>
        public bool? DeleteBranchOnMerge { get; set; }

        /// <summary>
        /// Optional. Automatically set the title of squashed commits to be the PR title. The default is null (do not update). The default when created is false.
        /// </summary>
        public bool? UseSquashPrTitleAsDefault { get; set; }

        /// <summary>
        /// Optional. True to archive this repository.  Note: you cannot unarchive repositories through the API. The default is null (do not update). The default when created is false.
        /// </summary>
        public bool? Archived { get; set; }

        /// <summary>
        /// Optional. Get or set whether to allow this repository to be forked or not. The default is null (do not update). The default when created is false.
        /// </summary>
        public bool? AllowForking { get; set; }

        internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
    }
}
