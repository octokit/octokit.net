using Octokit.Internal;

namespace Octokit
{
    public enum AccountType
    {
        /// <summary>
        ///  User account
        /// </summary>
        [Parameter(Value = "user")]
        User,

        /// <summary>
        /// Organization account
        /// </summary>
        [Parameter(Value = "organization")]
        Organization,

        /// <summary>
        /// Bot account
        /// </summary>
        [Parameter(Value = "bot")]
        Bot
    }
}
