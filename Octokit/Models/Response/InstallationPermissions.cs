using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// The permissions granted to the user-to-server access token.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InstallationPermissions
    {
        public InstallationPermissions() { }

        public InstallationPermissions
        (
            InstallationReadWritePermissionLevel? actions,
            InstallationReadWritePermissionLevel? administration,
            InstallationReadWritePermissionLevel? checks,
            InstallationReadWritePermissionLevel? contents,
            InstallationReadWritePermissionLevel? deployments,
            InstallationReadWritePermissionLevel? environments,
            InstallationReadWritePermissionLevel? issues,
            InstallationReadWritePermissionLevel? metadata,
            InstallationReadWritePermissionLevel? packages,
            InstallationReadWritePermissionLevel? pages,
            InstallationReadWritePermissionLevel? pullRequests,
            InstallationReadWritePermissionLevel? repositoryAnnouncementBanners,
            InstallationReadWritePermissionLevel? repositoryCustomProperties,
            InstallationReadWritePermissionLevel? repositoryHooks,
            InstallationReadWriteAdminPermissionLevel? repositoryProjects,
            InstallationReadWritePermissionLevel? secretScanningAlerts,
            InstallationReadWritePermissionLevel? secrets,
            InstallationReadWritePermissionLevel? securityEvents,
            InstallationReadWritePermissionLevel? singleFile,
            InstallationReadWritePermissionLevel? statuses,
            InstallationReadWritePermissionLevel? vulnerabilityAlerts,
            InstallationWritePermissionLevel? workflows,
            InstallationReadWritePermissionLevel? members,
            InstallationReadWritePermissionLevel? organizationAdministration,
            InstallationReadWritePermissionLevel? organizationCopilotSeatManagement,
            InstallationReadWriteAdminPermissionLevel? organizationCustomProperties,
            InstallationReadWritePermissionLevel? organizationCustomRoles,
            InstallationReadWritePermissionLevel? organizationAnnouncementBanners,
            InstallationReadWritePermissionLevel? organizationHooks,
            InstallationReadPermissionLevel? organizationPlan,
            InstallationReadWriteAdminPermissionLevel? organizationProjects,
            InstallationReadWritePermissionLevel? organizationPackages,
            InstallationReadWritePermissionLevel? organizationSecrets,
            InstallationReadWritePermissionLevel? organizationSelfHostedRunners,
            InstallationReadWritePermissionLevel? organizationUserBlocking,
            InstallationReadWritePermissionLevel? teamDiscussions
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
            RepositoryCustomProperties = repositoryCustomProperties;
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
            OrganizationCopilotSeatManagement = organizationCopilotSeatManagement;
            OrganizationCustomProperties = organizationCustomProperties;
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
        public StringEnum<InstallationReadWritePermissionLevel>? Actions { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for repository creation, deletion, settings, teams, and collaborators creation.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Administration { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for checks on code.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Checks { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for repository contents, commits, branches, downloads, releases, and merges.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Contents { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for deployments and deployment statuses.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Deployments { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for managing repository environments.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Environments { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for issues and related comments, assignees, labels, and milestones.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Issues { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to search repositories, list collaborators, and access repository metadata.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Metadata { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for packages published to GitHub Packages.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Packages { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to retrieve Pages statuses, configuration, and builds, as well as create new builds.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Pages { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for pull requests and related comments, assignees, labels, milestones, and merges.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? PullRequests { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage announcement banners for a repository.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? RepositoryAnnouncementBanners { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and set values for a repository's custom properties, when allowed by the property.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? RepositoryCustomProperties { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage the post-receive hooks for a repository.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? RepositoryHooks { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage repository projects, columns, and cards.
        /// </summary>
        public StringEnum<InstallationReadWriteAdminPermissionLevel>? RepositoryProjects { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage secret scanning alerts.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? SecretScanningAlerts { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage repository secrets.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Secrets { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage security events like code scanning alerts.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? SecurityEvents { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage just a single file.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? SingleFile { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for commit statuses.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Statuses { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage Dependabot alerts.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? VulnerabilityAlerts { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to update GitHub Actions workflow files.
        /// </summary>
        public StringEnum<InstallationWritePermissionLevel>? Workflows { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for organization teams and members.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? Members { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage access to an organization.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationAdministration { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage access Copilot Business seats and settings.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationCopilotSeatManagement { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view custom properties, write repository values, and administer definitions.
        /// </summary>
        public StringEnum<InstallationReadWriteAdminPermissionLevel>? OrganizationCustomProperties { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for custom roles management. This property is in beta and is subject to change.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationCustomRoles { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage announcement banners for an organization.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationAnnouncementBanners { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage the post-receive hooks for an organization.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationHooks { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for viewing an organization's plan.
        /// </summary>
        public StringEnum<InstallationReadPermissionLevel>? OrganizationPlan { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage organization projects and projects beta (where available).
        /// </summary>
        public StringEnum<InstallationReadWriteAdminPermissionLevel>? OrganizationProjects { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token for organization packages published to GitHub Packages.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationPackages { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage organization secrets.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationSecrets { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage GitHub Actions self-hosted runners available to an organization.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationSelfHostedRunners { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to view and manage users blocked by the organization.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? OrganizationUserBlocking { get; private set; }

        /// <summary>
        /// The level of permission to grant the access token to manage team discussions and related comments.
        /// </summary>
        public StringEnum<InstallationReadWritePermissionLevel>? TeamDiscussions { get; private set; }

        internal string DebuggerDisplay => $"Actions: {Actions}, Administration: {Administration}, Checks: {Checks}, Contents: {Contents}, Deployments: {Deployments}, Environments: {Environments}, Issues: {Issues}, Metadata: {Metadata}, Packages: {Packages}, Pages: {Pages}, PullRequests: {PullRequests}, RepositoryAnnouncementBanners: {RepositoryAnnouncementBanners}, RepositoryHooks: {RepositoryHooks}, RepositoryProjects: {RepositoryProjects}, SecretScanningAlerts: {SecretScanningAlerts}, Secrets: {Secrets}, SecurityEvents: {SecurityEvents}, SingleFile: {SingleFile}, Statuses: {Statuses}, VulnerabilityAlerts: {VulnerabilityAlerts}, Workflows: {Workflows}, Members: {Members}, OrganizationAdministration: {OrganizationAdministration}, OrganizationCustomRoles: {OrganizationCustomRoles}, OrganizationAnnouncementBanners: {OrganizationAnnouncementBanners}, OrganizationHooks: {OrganizationHooks}, OrganizationPlan: {OrganizationPlan}, OrganizationProjects: {OrganizationProjects}, OrganizationPackages: {OrganizationPackages}, OrganizationSecrets: {OrganizationSecrets}, OrganizationSelfHostedRunners: {OrganizationSelfHostedRunners}, OrganizationUserBlocking: {OrganizationUserBlocking}, TeamDiscussions: {TeamDiscussions}";
    }
}
