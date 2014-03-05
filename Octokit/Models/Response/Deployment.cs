using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces",
        Justification="People can use fully qualified names if they want to use both.")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Deployment
    {
        /// <summary>
        /// Id of this deployment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// The API URL for this deployment.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The <seealso cref="User"/> that created the deployment.
        /// </summary>
        public User Creator { get; set; }

        /// <summary>
        /// JSON payload with extra information about the deployment.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IDictionary<string, string> Payload { get; set; }

        /// <summary>
        /// Date and time that the deployment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Date and time that the deployment was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// A short description of the deployment.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The API URL for the <seealso cref="DeploymentStatus"/>es of this deployment.
        /// </summary>
        public string StatusesUrl { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "CreatedAt: {0}", CreatedAt);
            }
        }
    }
}