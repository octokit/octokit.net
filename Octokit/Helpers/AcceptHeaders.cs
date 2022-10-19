namespace Octokit
{
    public static class AcceptHeaders
    {
        public const string StableVersion = "application/vnd.github.v3";

        public const string StableVersionHtml = "application/vnd.github.v3.html";

        public const string StableVersionJson = "application/vnd.github.v3+json";

        /// <summary>
        /// Support for retrieving raw file content with the <see cref="IConnection.GetRaw"/> method.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/contents/#custom-media-types</remarks>
        public const string RawContentMediaType = "application/vnd.github.v3.raw";
    }
}
