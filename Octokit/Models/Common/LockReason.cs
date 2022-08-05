using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// The possible reasons that an issue or pull request was locked.
    /// </summary>
    public enum LockReason
    {
        // The issue or pull request was locked because the conversation was off-topic.
        [Parameter(Value = "off-topic")]
        OffTopic,

        // The issue or pull request was locked because the conversation was resolved.
        [Parameter(Value = "resolved")]
        Resolved,

        // The issue or pull request was locked because the conversation was spam.
        [Parameter(Value = "spam")]
        Spam,

        // The issue or pull request was locked because the conversation was too heated.
        [Parameter(Value = "too heated")]
        TooHeated
    }
}
