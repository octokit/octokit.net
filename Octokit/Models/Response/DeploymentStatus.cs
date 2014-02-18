using System;

namespace Octokit
{
    public class DeploymentStatus
    {
        /// <summary>
        /// Id of this deployment status.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The API URL for this deployment status.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The state of this deployment status.
        /// </summary>
        public DeploymentState State { get; set; }

        /// <summary>
        /// The <seealso cref="User"/> that created this deployment status.
        /// </summary>
        public User Creator { get; set; }

        /// <summary>
        /// JSON payload with extra information about the deployment.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// The target URL of this deployment status. This URL should contain
        /// output to keep the user updated while the task is running or serve
        /// as historical information for what happened in the deployment
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// The date and time that the status was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date and time that the status was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// A short description of the status.
        /// </summary>
        public string Description { get; set; }
    }

    public enum DeploymentState
    {
        Pending,
        Success,
        Error,
        Failure
    }
}