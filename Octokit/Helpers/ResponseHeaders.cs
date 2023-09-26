namespace Octokit
{
    public static class ResponseHeaders
    {
        public const string RateLimitLimit = "x-ratelimit-limit";
        public const string RateLimitRemaining = "x-ratelimit-remaining";
        public const string RateLimitUsed = "x-ratelimit-used";
        public const string RateLimitReset = "x-ratelimit-reset";
    }
}
