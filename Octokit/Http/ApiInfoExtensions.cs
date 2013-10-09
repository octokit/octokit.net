using System;

namespace Octokit
{
    public static class ApiInfoExtensions
    {
        public static Uri GetPreviousPageUrl(this ApiInfo info)
        {
            Ensure.ArgumentNotNull(info, "info");

            return info.Links.SafeGet("prev");
        }

        public static Uri GetNextPageUrl(this ApiInfo info)
        {
            Ensure.ArgumentNotNull(info, "info");

            return info.Links.SafeGet("next");
        }

        public static Uri GetFirstPageUrl(this ApiInfo info)
        {
            Ensure.ArgumentNotNull(info, "info");

            return info.Links.SafeGet("first");
        }

        public static Uri GetLastPageUrl(this ApiInfo info)
        {
            Ensure.ArgumentNotNull(info, "info");

            return info.Links.SafeGet("last");
        }
    }
}
