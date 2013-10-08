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
        public int Collaborators { get; set; }

        /// <summary>
        /// The name of the plan.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of private repositories allowed with this plan.
        /// </summary>
        public int PrivateRepos { get; set; }

        /// <summary>
        /// The amount of disk space allowed with this plan.
        /// </summary>
        public int Space { get; set; }

        /// <summary>
        /// The billing email for the organization. Only has a value in response to editing an organization.
        /// </summary>
        public string BillingEmail { get; set; }
    }
}