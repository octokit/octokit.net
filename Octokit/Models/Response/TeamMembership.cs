using System;

namespace Octokit
{
    [Obsolete("Please use TeamMembershipDetails response class instead")]
    public enum TeamMembership
    {
        NotFound = 0,
        Pending = 1,
        Active = 2
    }
}
