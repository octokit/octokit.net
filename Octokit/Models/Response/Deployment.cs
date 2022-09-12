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

        public Deployment(int id, string nodeId, string sha, string @ref, string url, User creator, IReadOnlyDictionary<string, string> payload, DateTimeOffset createdAt, DateTimeOffset updatedAt, string description, string statusesUrl, string repositoryUrl, string environment, string originalEnvironment, bool transientEnvironment, bool productionEnvironment, string task)
        {
            Id = id;
            NodeId = nodeId;
            Sha = sha;
            Ref = @ref;
            Url = url;
            Creator = creator;
            Payload = payload;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Description = description;
            StatusesUrl = statusesUrl;
            RepositoryUrl = repositoryUrl;
            Environment = environment;
            OriginalEnvironment = originalEnvironment;
            TransientEnvironment = transientEnvironment;
            ProductionEnvironment = productionEnvironment;
            Task = task;
        }

        /// <summary>
        /// Id of this deployment.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The SHA recorded at creation time.
        /// </summary>
        public string Sha { get; private set; }

        /// <summary>
        /// The name of the ref. This can be a branch, tag, or SHA.
        /// </summary>
        public string Ref { get; private set; }

        /// <summary>
        /// The API URL for this deployment.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The <seealso cref="User"/> that created the deployment.
        /// </summary>
        public User Creator { get; private set; }

        /// <summary>
        /// JSON payload with extra information about the deployment.
        /// </summary>
        public IReadOnlyDictionary<string, string> Payload { get; private set; }

        /// <summary>
        /// Date and time that the deployment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Date and time that the deployment was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        /// <summary>
        /// A short description of the deployment.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The API URL for the <seealso cref="DeploymentStatus"/>es of this deployment.
        /// </summary>
        public string StatusesUrl { get; private set; }

        /// <summary>
        /// The API URL for the <seealso cref="Repository"/> of this deployment.
        /// </summary>
        public string RepositoryUrl { get; private set; }

        /// <summary>
        /// The name of the <seealso cref="Environment"/> that was deployed to (e.g., staging or production).
        /// </summary>
        public string Environment { get; private set; }

        /// <summary>
        /// The name of the that was originally deployed to (e.g., staging or production).
        /// </summary>
        public string OriginalEnvironment { get; private set; }

        /// <summary>
        /// Indicates if the environment is specific to a deployment and will no longer exist at some point in the future.
        /// </summary>
        public bool TransientEnvironment { get; private set; }

        /// <summary>
        /// Indicates if the environment is one with which end users directly interact.
        /// </summary>
        public bool ProductionEnvironment { get; private set; }

        /// <summary>
        /// Specifies a task to execute (e.g., deploy or deploy:migrations)
        /// </summary>
        public string Task { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "CreatedAt: {0}", CreatedAt);
            }
        }
    }
}
