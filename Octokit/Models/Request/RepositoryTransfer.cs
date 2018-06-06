using System;

namespace Octokit
{
    public class RepositoryTransfer
    {
        public RepositoryTransfer(string newOwner)
        {
            Ensure.ArgumentNotNullOrEmptyString(newOwner, nameof(newOwner));

            NewOwner = newOwner;
        }

        public RepositoryTransfer(string newOwner, int[] teamId)
            : this(newOwner)
        {
            // TODO Create Ensure method to check for empty arrays
            // Ensure.ArgumentNotNullOrEmptyArray(teamId, nameof(teamId));

            TeamId = new int[teamId.Length];
            Array.Copy(teamId, TeamId, teamId.Length);
        }

        public string NewOwner { get; set; }

        public int[] TeamId { get; set; }
    }
}