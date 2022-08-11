using Octokit.Internal;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// A plan (either paid or free) for a particular user
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Plan
    {
        public Plan() { }

        public Plan(long collaborators, string name, long privateRepos, long space, string billingEmail, int filledSeats, int seats)
        {
            Collaborators = collaborators;
            Name = name;
            PrivateRepos = privateRepos;
            Space = space;
            BillingEmail = billingEmail;
            FilledSeats = filledSeats;
            Seats = seats;
        }

        /// <summary>
        /// The number of collaborators allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" number of collaborators.</remarks>
        public long Collaborators { get; private set; }

        /// <summary>
        /// The name of the plan.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The number of private repositories allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" number of plans.</remarks>
        public long PrivateRepos { get; private set; }

        /// <summary>
        /// The amount of disk space allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" amount of disk space.</remarks>
        public long Space { get; private set; }

        /// <summary>
        /// The billing email for the organization. Only has a value in response to editing an organization.
        /// </summary>
        public string BillingEmail { get; private set; }

        /// <summary>
        /// The number of seats filled on this plan
        /// </summary>
        public int FilledSeats { get; private set; } 

        /// <summary>
        /// The number of seats available for this plan
        /// </summary>
        public int Seats { get; private set; }

        internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
    }
}
