namespace Octokit
{
    public enum TeamMembership
    {
        NotFound = 0,
        Pending = 1,
        Active = 2,
        /// <summary>
        /// Used as a placeholder for unknown fields
        /// </summary>
        UnknownType
    }
}
