using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeploymentStatus
    {
        public DeploymentStatus() { }

        public DeploymentStatus(int id, string nodeId, string url, DeploymentState state, User creator, IReadOnlyDictionary<string, string> payload, string targetUrl, string logUrl, string environmentUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt, string description)
        {
            Id = id;
            NodeId = nodeId;
            Url = url;
            State = state;
            Creator = creator;
            Payload = payload;
            TargetUrl = targetUrl;
            LogUrl = logUrl;
            EnvironmentUrl = environmentUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Description = description;
        }

        /// <summary>
        /// Id of this deployment status.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// The API URL for this deployment status.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The state of this deployment status.
        /// </summary>
        public StringEnum<DeploymentState> State { get; protected set; }

        /// <summary>
        /// The <seealso cref="User"/> that created this deployment status.
        /// </summary>
        public User Creator { get; protected set; }

        /// <summary>
        /// JSON payload with extra information about the deployment.
        /// </summary>
        public IReadOnlyDictionary<string, string> Payload { get; protected set; }

        /// <summary>
        /// The target URL of this deployment status. This URL should contain
        /// output to keep the user updated while the task is running or serve
        /// as historical information for what happened in the deployment
        /// </summary>
        public string TargetUrl { get; protected set; }

        /// <summary>
        /// The target URL  of this deployment status. This URL should contain
        /// output to keep the user updated while the task is running or serve as
        /// historical information for what happened in the deployment
        /// </summary>
        public string LogUrl { get; protected set; }

        /// <summary>
        /// The URL for accessing your environment.
        /// </summary>
        public string EnvironmentUrl { get; protected set; }

        /// <summary>
        /// The date and time that the status was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The date and time that the status was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        /// <summary>
        /// A short description of the status.
        /// </summary>
        public string Description { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "State: {0} UpdatedAt: {1}", State, UpdatedAt);
            }
        }
    }

    public enum DeploymentState
    {
        [Parameter(Value = "pending")]
        Pending,

        [Parameter(Value = "success")]
        Success,

        [Parameter(Value = "error")]
        Error,

        [Parameter(Value = "failure")]
        Failure,

        [Parameter(Value = "inactive")]
        Inactive,

        [Parameter(Value = "in_progress")]
        InProgress,

        [Parameter(Value = "queued")]
        Queued
    }
}