using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public static class AcceptHeaders
    {
        public const string StableVersion = "application/vnd.github.v3";

        public const string StableVersionHtml = "application/vnd.github.v3.html";

        public const string StableVersionJson = "application/vnd.github.v3+json";

        public const string CommitReferenceSha1MediaType = "application/vnd.github.v3.sha";

        /// <summary>
        /// Support for retrieving raw file content with the <see cref="IConnection.GetRaw"/> method.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#custom-media-types</remarks>
        public const string RawContentMediaType = "application/vnd.github.v3.raw";

        public const string StarCreationTimestamps = "application/vnd.github.v3.star+json";

        public const string MigrationsApiPreview = "application/vnd.github.wyandotte-preview+json";

        public const string ReactionsPreview = "application/vnd.github.squirrel-girl-preview+json";

        public const string DeploymentApiPreview = "application/vnd.github.ant-man-preview+json";

        public const string IssueTimelineApiPreview = "application/vnd.github.mockingbird-preview+json";

        public const string DraftPullRequestApiPreview = "application/vnd.github.shadow-cat-preview+json";

        public const string ProjectsApiPreview = "application/vnd.github.inertia-preview+json";

        [Obsolete("API is considered legacy")]
        public const string OrganizationMembershipPreview = "application/vnd.github.korra-preview+json";

        public const string GitHubAppsPreview = "application/vnd.github.machine-man-preview+json";

        public const string PreReceiveEnvironmentsPreview = "application/vnd.github.eye-scream-preview+json";

        public const string ChecksApiPreview = "application/vnd.github.antiope-preview+json";

        public const string ProtectedBranchesRequiredApprovingApiPreview = "application/vnd.github.luke-cage-preview+json";

        public const string IssueEventsApiPreview = "application/vnd.github.starfox-preview+json";

        public const string DeploymentStatusesPreview = "application/vnd.github.flash-preview+json";

        public const string OAuthApplicationsPreview = "application/vnd.github.doctor-strange-preview+json";

        public const string RepositoryTopicsPreview = "application/vnd.github.mercy-preview+json";

        public const string VisibilityPreview = "application/vnd.github.nebula-preview+json";

        /// <summary>
        /// Combines multiple preview headers. GitHub API supports Accept header with multiple
        /// values separated by comma.
        /// </summary>
        /// <param name="headers">Accept header values that will be combine to single Accept header.</param>
        /// <remarks>
        /// This Accept header <c>application/vnd.github.loki-preview+json,application/vnd.github.drax-preview+json</c>
        /// indicated we want both Protected Branches and Licenses preview APIs.
        /// </remarks>
        /// <returns>Accept header value.</returns>
        public static string Concat(params string[] headers)
        {
            return string.Join(",", headers);
        }
    }
}
