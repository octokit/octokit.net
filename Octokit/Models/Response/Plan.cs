namespace Octokit
{
    /// <summary>
    /// A plan (either paid or free) for a particular user
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// The number of collaborators allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" number of collaborators.</remarks>
        public long Collaborators { get; set; }

        /// <summary>
        /// The name of the plan.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of private repositories allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" number of plans.</remarks>
        public long PrivateRepos { get; set; }

        /// <summary>
        /// The amount of disk space allowed with this plan.
        /// </summary>
        /// <remarks>This returns <see cref="long"/> because GitHub Enterprise uses a sentinel value of 999999999999 to denote an "unlimited" amount of disk space.</remarks>
        public long Space { get; set; }

        /// <summary>
        /// The billing email for the organization. Only has a value in response to editing an organization.
        /// </summary>
        public string BillingEmail { get; set; }
    }
}