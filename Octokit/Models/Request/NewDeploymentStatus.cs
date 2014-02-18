namespace Octokit
{
    /// <summary>
    /// Describes a new deployment status to create.
    /// </summary>
    public class NewDeploymentStatus
    {
        /// <summary>
        /// The state of the status.
        /// </summary>
        public DeploymentState State { get; set; }

        /// <summary>
        /// The target URL to associate with this status. This URL should contain
        /// output to keep the user updated while the task is running or serve as
        /// historical information for what happened in the deployment
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// A short description of the status.
        /// </summary>
        public string Description { get; set; }
    }
}