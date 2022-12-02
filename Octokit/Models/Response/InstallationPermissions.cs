using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InstallationPermissions
    {
        public InstallationPermissions() { }

        public InstallationPermissions
        (
            InstallationPermissionLevel? actions,
            InstallationPermissionLevel? administration,
            InstallationPermissionLevel? checks,
            InstallationPermissionLevel? contents,
            InstallationPermissionLevel? deployments,
            InstallationPermissionLevel? environments,
            InstallationPermissionLevel? issues,
            InstallationPermissionLevel? metadata,
            InstallationPermissionLevel? packages,
            InstallationPermissionLevel? pages,
            InstallationPermissionLevel? pullRequests,
            InstallationPermissionLevel? repositoryAnnouncementBanners,
            InstallationPermissionLevel? repositoryHooks,
            InstallationPermissionLevel? repositoryProjects,
            InstallationPermissionLevel? secretScanningAlerts,
            InstallationPermissionLevel? secrets,
            InstallationPermissionLevel? securityEvents,
            InstallationPermissionLevel? singleFile,
            InstallationPermissionLevel? statuses,
            InstallationPermissionLevel? vulnerabilityAlerts,
            InstallationPermissionLevel? workflows,
            InstallationPermissionLevel? members,
            InstallationPermissionLevel? organizationAdministration,
            InstallationPermissionLevel? organizationCustomRoles,
            InstallationPermissionLevel? organizationAnnouncementBanners,
            InstallationPermissionLevel? organizationHooks,
            InstallationPermissionLevel? organizationPlan,
            InstallationPermissionLevel? organizationProjects,
            InstallationPermissionLevel? organizationPackages,
            InstallationPermissionLevel? organizationSecrets,
            InstallationPermissionLevel? organizationSelfHostedRunners,
            InstallationPermissionLevel? organizationUserBlocking,
            InstallationPermissionLevel? teamDiscussions
        )
        {
            Actions = actions;
            Administration = administration;
            Checks = checks;
            Contents = contents;
            Deployments = deployments;
            Environments = environments;
            Issues = issues;
            Metadata = metadata;
            Packages = packages;
            Pages = pages;
            PullRequests = pullRequests;
            RepositoryAnnouncementBanners = repositoryAnnouncementBanners;
            RepositoryHooks = repositoryHooks;
            RepositoryProjects = repositoryProjects;
            SecretScanningAlerts = secretScanningAlerts;
            Secrets = secrets;
            SecurityEvents = securityEvents;
            SingleFile = singleFile;
            Statuses = statuses;
            VulnerabilityAlerts = vulnerabilityAlerts;
            Workflows = workflows;
            Members = members;
            OrganizationAdministration = organizationAdministration;
            OrganizationCustomRoles = organizationCustomRoles;
            OrganizationAnnouncementBanners = organizationAnnouncementBanners;
            OrganizationHooks = organizationHooks;
            OrganizationPlan = organizationPlan;
            OrganizationProjects = organizationProjects;
            OrganizationPackages = organizationPackages;
            OrganizationSecrets = organizationSecrets;
            OrganizationSelfHostedRunners = organizationSelfHostedRunners;
            OrganizationUserBlocking = organizationUserBlocking;
            TeamDiscussions = teamDiscussions;
        }

        /// <summary>
        /// The level of permission to grant the access token for GitHub Actions workflows, workflow runs, and artifacts.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Actions { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for repository creation, deletion, settings, teams, and collaborators creation.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Administration { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for checks on code.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Checks { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for repository contents, commits, branches, downloads, releases, and merges.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Contents { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for deployments and deployment statuses.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Deployments { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for managing repository environments.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Environments { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for issues and related comments, assignees, labels, and milestones.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Issues { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to search repositories, list collaborators, and access repository metadata.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Metadata { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for packages published to GitHub Packages.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Packages { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to retrieve Pages statuses, configuration, and builds, as well as create new builds.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Pages { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for pull requests and related comments, assignees, labels, milestones, and merges.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? PullRequests { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage announcement banners for a repository.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? RepositoryAnnouncementBanners { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage the post-receive hooks for a repository.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? RepositoryHooks { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage repository projects, columns, and cards.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? RepositoryProjects { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage secret scanning alerts.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? SecretScanningAlerts { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage repository secrets.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Secrets { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage security events like code scanning alerts.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? SecurityEvents { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage just a single file.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? SingleFile { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for commit statuses.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Statuses { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage Dependabot alerts.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? VulnerabilityAlerts { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to update GitHub Actions workflow files.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Workflows { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for organization teams and members.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? Members { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage access to an organization.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationAdministration { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for custom roles management. This property is in beta and is subject to change.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationCustomRoles { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage announcement banners for an organization.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationAnnouncementBanners { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage the post-receive hooks for an organization.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationHooks { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for viewing an organization's plan.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationPlan { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage organization projects and projects beta (where available).
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationProjects { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for organization packages published to GitHub Packages.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationPackages { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage organization secrets.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationSecrets { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage GitHub Actions self-hosted runners available to an organization.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationSelfHostedRunners { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage users blocked by the organization.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? OrganizationUserBlocking { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage team discussions and related comments.
        /// </summary>
        public StringEnum<InstallationPermissionLevel>? TeamDiscussions { get; private set; }

        internal string DebuggerDisplay
        {
            get => $"Actions: {Actions}, Administration: {Administration}, Checks: {Checks}, Contents: {Contents}, Deployments: {Deployments}, Environments: {Environments}, Issues: {Issues}, Metadata: {Metadata}, Packages: {Packages}, Pages: {Pages}, PullRequests: {PullRequests}, RepositoryAnnouncementBanners: {RepositoryAnnouncementBanners}, RepositoryHooks: {RepositoryHooks}, RepositoryProjects: {RepositoryProjects}, SecretScanningAlerts: {SecretScanningAlerts}, Secrets: {Secrets}, SecurityEvents: {SecurityEvents}, SingleFile: {SingleFile}, Statuses: {Statuses}, VulnerabilityAlerts: {VulnerabilityAlerts}, Workflows: {Workflows}, Members: {Members}, OrganizationAdministration: {OrganizationAdministration}, OrganizationCustomRoles: {OrganizationCustomRoles}, OrganizationAnnouncementBanners: {OrganizationAnnouncementBanners}, OrganizationHooks: {OrganizationHooks}, OrganizationPlan: {OrganizationPlan}, OrganizationProjects: {OrganizationProjects}, OrganizationPackages: {OrganizationPackages}, OrganizationSecrets: {OrganizationSecrets}, OrganizationSelfHostedRunners: {OrganizationSelfHostedRunners}, OrganizationUserBlocking: {OrganizationUserBlocking}, TeamDiscussions: {TeamDiscussions}";
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
