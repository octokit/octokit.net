using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Text;
using System.Globalization;

namespace Octokit.Models.Response
{
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces",
        Justification = "People can use fully qualified names if they want to use both.")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeploymentEnvironment
    {
        public DeploymentEnvironment() { }

        public DeploymentEnvironment(long id, string nodeID, string name, string url, string htmlUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            Id = id;
            NodeId = nodeID;
            Name = name;
            Url = url;
            HtmlUrl = htmlUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// Id of this deployment environment
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId{ get; private set; }

        /// <summary>
        /// Environment Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Environment API URL
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Environment HTML URL
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// Date and time that the environment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Date and time that the environment was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, CreatedAt: {1}", Name, CreatedAt);
            }
        }
    }
}