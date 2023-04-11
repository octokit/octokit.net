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
        Bot,

	/// <summary>
	/// Mannequin account - all user activity in the migrated repository (except Git commits) 
	/// 			is attributed to placeholder identities called mannequins.
	/// </summary>
	[Parameter(Value = "Mannequin")]
	Mannequin
    }
}
