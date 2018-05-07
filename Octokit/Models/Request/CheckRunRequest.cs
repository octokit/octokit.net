using Octokit.Internal;

namespace Octokit
{
    public sealed class CheckRunRequest
    {
        public string Ref { get; set; }
        public string CheckName { get; set; }
        public CheckStatus? Status { get; set; }
        public CheckRunRequestFilter? Filter { get; set; }
    }

    public enum CheckRunRequestFilter
    {
        [Parameter(Value = "latest")]
        Latest,
        [Parameter(Value = "all")]
        All
    }
}
