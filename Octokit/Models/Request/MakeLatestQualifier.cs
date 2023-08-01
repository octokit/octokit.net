using Octokit.Internal;

namespace Octokit
{
    public enum MakeLatestQualifier
    {
        [Parameter(Value = "true")]
        True = 1,
        [Parameter(Value = "false")]
        False = 2,
        [Parameter(Value = "legacy")]
        Legacy = 3,
    }
}
