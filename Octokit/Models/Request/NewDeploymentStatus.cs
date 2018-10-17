using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new deployment status to create.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDeploymentStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewDeploymentStatus"/> class.
        /// </summary>
        /// <param name="deploymentState">State of the deployment (Required).</param>
        public NewDeploymentStatus(DeploymentState deploymentState)
        {
            State = deploymentState;
        }

        /// <summary>
        /// The state of the status.
        /// </summary>
        public DeploymentState State { get; private set; }

        /// <summary>
        /// The target URL to associate with this status. This URL should contain
        /// output to keep the user updated while the task is running or serve as
        /// historical information for what happened in the deployment
        /// </summary>
        public string LogUrl { get; set; }

        /// <summary>
        /// A short description of the status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The URL for accessing your environment.
        /// </summary>
        public string EnvironmentUrl { get; set; }

        /// <summary>
        /// Name for the target deployment environment.
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Indicates if a new inactive status should be added to all non-transient, 
        /// non-production environment deployments with the same repository and environment 
        /// name as the created status's deployment. 
        /// (DEFAULT if not specified: True)
        /// </summary>
        public bool? AutoInactive { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "State: {0}", State);
            }
        }
    }
}