namespace Octokit
{
    public static class AcceptHeaders
    {
        public const string StableVersion = "application/vnd.github.v3";

        public const string StableVersionHtml = "application/vnd.github.html";

        public const string RedirectsPreviewThenStableVersionJson = "application/vnd.github.quicksilver-preview+json; charset=utf-8, application/vnd.github.v3+json; charset=utf-8";

        public const string LicensesApiPreview = "application/vnd.github.drax-preview+json";

        public const string ProtectedBranchesApiPreview = "application/vnd.github.loki-preview+json";

        public const string StarCreationTimestamps = "application/vnd.github.v3.star+json";
    }
}
