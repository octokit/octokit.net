﻿using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InstallationPermissions
    {
        public InstallationPermissions() { }

        public InstallationPermissions(InstallationPermissionLevel? metadata, InstallationPermissionLevel? administration, InstallationPermissionLevel? statuses, InstallationPermissionLevel? deployments, InstallationPermissionLevel? issues, InstallationPermissionLevel? pages, InstallationPermissionLevel? pullRequests, InstallationPermissionLevel? contents, InstallationPermissionLevel? singleFile, InstallationPermissionLevel? repositoryProjects, InstallationPermissionLevel? members, InstallationPermissionLevel? organizationProjects, InstallationPermissionLevel? teamDiscussions)
        {
            Metadata = metadata;
            Administration = administration;
            Statuses = statuses;
            Deployments = deployments;
            Issues = issues;
            Pages = pages;
            PullRequests = pullRequests;
            Contents = contents;
            SingleFile = singleFile;
            RepositoryProjects = repositoryProjects;
            Members = members;
            OrganizationProjects = organizationProjects;
            TeamDiscussions = teamDiscussions;
        }
        
        /// <summary>
        /// Repository metadata
        /// Search repositories, list collaborators, and access repository metadata.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Metadata { get; protected set; }

        /// <summary>
        /// Repository administration
        /// Repository creation, deletion, settings, teams, and collaborators.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Administration { get; protected set; }

        /// <summary>
        /// Commit statuses
        /// Commit statuses.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Statuses { get; protected set; }

        /// <summary>
        /// Deployments
        /// Deployments and deployment statuses.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Deployments { get; protected set; }

        /// <summary>
        /// Issues
        /// Issues and related comments, assignees, labels, and milestones.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Issues { get; protected set; }

        /// <summary>
        /// Pages
        /// Retrieve Pages statuses, configuration, and builds, as well as create new builds.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Pages { get; protected set; }

        /// <summary>
        /// Pull requests
        /// Pull requests and related comments, assignees, labels, milestones, and merges.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? PullRequests { get; protected set; }

        /// <summary>
        /// Repository contents
        /// Repository contents, commits, branches, downloads, releases, and merges.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Contents { get; protected set; }

        /// <summary>
        /// Single file
        /// Manage just a single file.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? SingleFile { get; protected set; }

        /// <summary>
        /// Repository projects
        /// Manage repository projects, columns, and cards.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? RepositoryProjects { get; protected set; }

        /// <summary>
        /// Organization members (only applicable when installed for an Organization )
        /// Organization members and teams.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Members { get; protected set; }

        /// <summary>
        /// Organization projects (only applicable when installed for an Organization )
        /// Manage organization projects, columns, and cards.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationProjects { get; protected set; }

        /// <summary>
        /// Team discussions (only applicable when installed for an Organization )
        /// Team discussions.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? TeamDiscussions { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Metadata: {0}, Contents: {1}, Issues: {2}, Single File: {3}", Metadata, Contents, Issues, SingleFile); }
        }
    }

    public enum InstallationPermissionLevel
    {
        [Parameter(Value = "read")]
        Read,

        [Parameter(Value = "write")]
        Write
    }
}