using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public static class AcceptHeaders
    {
        public const string StableVersion = "application/vnd.github.v3";

        public const string StableVersionHtml = "application/vnd.github.html";

        public const string RedirectsPreviewThenStableVersionJson = "application/vnd.github.quicksilver-preview+json; charset=utf-8, application/vnd.github.v3+json; charset=utf-8";

        public const string CommitReferenceSha1MediaType = "application/vnd.github.v3.sha";

        public const string OrganizationPermissionsPreview = "application/vnd.github.ironman-preview+json";

        public const string LicensesApiPreview = "application/vnd.github.drax-preview+json";

        public const string ProtectedBranchesApiPreview = "application/vnd.github.loki-preview+json";

        public const string StarCreationTimestamps = "application/vnd.github.v3.star+json";

        public const string IssueLockingUnlockingApiPreview = "application/vnd.github.the-key-preview+json";

        public const string SquashCommitPreview = "application/vnd.github.polaris-preview+json";

        public const string MigrationsApiPreview = "application/vnd.github.wyandotte-preview+json";

        public const string ReactionsPreview = "application/vnd.github.squirrel-girl-preview";

        public const string SignatureVerificationPreview = "application/vnd.github.cryptographer-preview+sha";

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
        public const string GpgKeysPreview = "application/vnd.github.cryptographer-preview";

        public const string DeploymentApiPreview = "application/vnd.github.ant-man-preview+json";

        public const string InvitationsApiPreview = "application/vnd.github.swamp-thing-preview+json";

        public const string PagesApiPreview = "application/vnd.github.mister-fantastic-preview+json";

        public const string IssueTimelineApiPreview = "application/vnd.github.mockingbird-preview";

        public const string RepositoryTrafficApiPreview = "application/vnd.github.spiderman-preview";

        public const string PullRequestReviewsApiPreview = "application/vnd.github.black-cat-preview+json";

        public const string ProjectsApiPreview = "application/vnd.github.inertia-preview+json";

        public const string OrganizationMembershipPreview = "application/vnd.github.korra-preview+json";

        public const string NestedTeamsPreview = "application/vnd.github.hellcat-preview+json";

        public const string MachineManPreview = "application/vnd.github.machine-man-preview+json";
    }
}
