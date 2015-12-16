
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Describes a new deployment status to create. Deployments are a request for a specific ref(branch,sha,tag) to
    /// be deployed.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/repos/deployments/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDeployment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewDeployment"/> class.
        /// </summary>
        /// <param name="ref">The ref to deploy.</param>
        public NewDeployment(string @ref)
        {
            Ref = @ref;
        }

        /// <summary>
        /// The ref to deploy. This can be a branch, tag, or sha. (REQUIRED)
        /// </summary>
        public string Ref { get; private set; }

        /// <summary>
        /// Gets or sets the optional task used to specify a task to execute, e.g. deploy or deploy:migrations. 
        /// Default: deploy
        /// </summary>
        /// <value>
        /// The task.
        /// </value>
        public DeployTask Task { get; set; }

        /// <summary>
        /// Merges the default branch into the requested deployment branch if true;
        /// Does nothing if false. (DEFAULT if not specified: True)
        /// </summary>
        public bool? AutoMerge { get; set; }

        /// <summary>
        /// Optional array of status contexts verified against commit status checks. If this property is null then 
        /// all unique contexts will be verified before a deployment is created. To bypass checking entirely, set this
        /// to an empty collection. Defaults to all unique contexts (aka null).
        /// </summary>
        /// <value>
        /// The required contexts.
        /// </value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Collection<string> RequiredContexts { get; set; }

        /// <summary>
        /// JSON payload with extra information about the deployment.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Optional name for the target deployment environment (e.g., production, staging, qa). Default: "production"
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public string Environment { get; set; }

        /// <summary>
        /// A short description of the deployment.
        /// </summary>
        public string Description { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Description: {0}", Description);
            }
        }
    }

    /// <summary>
    /// The types of deployments tasks that are availabel.
    /// </summary>
    public enum DeployTask
    {
        /// <summary>
        /// Deploy everything (default)
        /// </summary>
        Deploy,

        /// <summary>
        /// Deploy migrations only.
        /// </summary>
        [Parameter(Value = "deploy:migrations")]
        DeployMigrations
    }
}
