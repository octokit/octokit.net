using Octokit.Internal;

namespace Octokit
{
    public enum AccountType
    {
        /// <summary>
        ///  User account
        /// </summary>
        [Parameter(Value = "User")]
        User,

        /// <summary>
        /// Organization account
        /// </summary>
        [Parameter(Value = "Organization")]
        Organization,

        /// <summary>
        /// Bot account
        /// </summary>
        [Parameter(Value = "Bot")]
        Bot
    }
}
