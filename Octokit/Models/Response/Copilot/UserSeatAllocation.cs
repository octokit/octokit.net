namespace Octokit
{
    /// <summary>
    /// Holds information about user names to be added or removed from a Copilot-enabled organization.
    /// </summary>
    public class UserSeatAllocation
    {
        /// <summary>
        /// One or more usernames to be added or removed from a Copilot-enabled organization.
        /// </summary>
        public string[] SelectedUsernames { get; set; }
    }
}