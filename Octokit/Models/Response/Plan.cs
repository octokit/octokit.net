using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A plan (either paid or free) for a particular user
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Plan
    {
        public Plan() { }

        public Plan(long collaborators, string name, long privateRepos, long space, string billingEmail)
        {
            Collaborators = collaborators;
            Name = name;
            PrivateRepos = privateRepos;
            Space = space;
            BillingEmail = billingEmail;
        }

        /// <summary>
        /// The number of collaborators allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" number of collaborators.</remarks>
        public long Collaborators { get; protected set; }

        /// <summary>
        /// The name of the plan.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The number of private repositories allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" number of plans.</remarks>
        public long PrivateRepos { get; protected set; }

        /// <summary>
        /// The amount of disk space allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" amount of disk space.</remarks>
        public long Space { get; protected set; }

        /// <summary>
        /// The billing email for the organization. Only has a value in response to editing an organization.
        /// </summary>
        public string BillingEmail { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Space: {1}, Private Repos: {2}, Collaborators: {3}", Name, Space, PrivateRepos, Collaborators); }
        }
    }
}
