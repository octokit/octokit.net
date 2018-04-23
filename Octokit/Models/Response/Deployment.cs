using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces",
        Justification = "People can use fully qualified names if they want to use both.")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Deployment
    {
        public Deployment() { }

        public Deployment(int id, string sha, string url, User creator, IReadOnlyDictionary<string, string> payload, DateTimeOffset createdAt, DateTimeOffset updatedAt, string description, string statusesUrl, bool transientEnvironment, bool productionEnvironment)
        {
            Id = id;
            Sha = sha;
            Url = url;
            Creator = creator;
            Payload = payload;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Description = description;
            StatusesUrl = statusesUrl;
            TransientEnvironment = transientEnvironment;
            ProductionEnvironment = productionEnvironment;
        }

        /// <summary>
        /// Id of this deployment.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// The API URL for this deployment.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The <seealso cref="User"/> that created the deployment.
        /// </summary>
        public User Creator { get; protected set; }

        /// <summary>
        /// JSON payload with extra information about the deployment.
        /// </summary>
        public IReadOnlyDictionary<string, string> Payload { get; protected set; }

        /// <summary>
        /// Date and time that the deployment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// Date and time that the deployment was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        /// <summary>
        /// A short description of the deployment.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The API URL for the <seealso cref="DeploymentStatus"/>es of this deployment.
        /// </summary>
        public string StatusesUrl { get; protected set; }

        /// <summary>
        /// Indicates if the environment is specific to a deployment and will no longer exist at some point in the future.
        /// </summary>
        public bool TransientEnvironment { get; protected set; }

        /// <summary>
        /// Indicates if the environment is one with which end users directly interact.
        /// </summary>
        public bool ProductionEnvironment { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "CreatedAt: {0}", CreatedAt);
            }
        }
    }
}