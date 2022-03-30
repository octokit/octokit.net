using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents an environment for a deployment approval.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PendingDeploymentEnvironment
    {
        public PendingDeploymentEnvironment() { }

        public PendingDeploymentEnvironment(long id, string nodeId, string name, string url, string htmlUrl)
        {
            Id = id;
            NodeId = nodeId;
            Name = name;
            Url = url;
            HtmlUrl = htmlUrl;
        }

        /// <summary>
        /// The Id of the environment.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id.
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// The name of the environment.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The URL for this environment.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The URL for the HTML view of this environment.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0}, Name: {1}", Id, Name);
            }
        }
    }
}
