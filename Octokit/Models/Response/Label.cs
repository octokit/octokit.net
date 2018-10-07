using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Label
    {
        public Label() { }

        public Label(string url, string name, string nodeId, string color, string description, bool @default)
        {
            Url = url;
            Name = name;
            NodeId = nodeId;
            Color = color;
            Description = description;
            Default = @default;
        }

        /// <summary>
        /// Url of the label
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Name of the label
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// Color of the label
        /// </summary>
        public string Color { get; protected set; }

        /// <summary>
        /// Description of the label
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Is default label
        /// </summary>
        public bool Default { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} Color: {1}", Name, Color); }
        }
    }
}
