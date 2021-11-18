using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// The state of enforcement for a hook.
    /// </summary>
    public enum PreReceiveHookEnforcement
    {
        /// <summary>
        /// Indicates the pre-receive hook will not run.
        /// </summary>
        [Parameter(Value = "disabled")]
        Disabled,

        /// <summary>
        /// Indicates it will run and reject any pushes that result in a non-zero status.
        /// </summary>
        [Parameter(Value = "enabled")]
        Enabled,

        /// <summary>
        /// Means the script will run but will not cause any pushes to be rejected.
        /// </summary>
        [Parameter(Value = "testing")]
        Testing
    }
}