using System;

namespace Octokit
{
    [Obsolete("This enum will be replaced with a response class TeamMembershipDetails")]
    public enum TeamMembership
    {
        NotFound = 0,
        Pending = 1,
        Active = 2
    }
}
