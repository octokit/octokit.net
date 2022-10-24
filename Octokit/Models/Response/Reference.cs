using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Reference
    {
        public Reference() { }

        public Reference(string @ref, string nodeId, string url, TagObject @object)
        {
            Ref = @ref;
            NodeId = nodeId;
            Url = url;
            Object = @object;
        }

        public string Ref { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public string Url { get; private set; }

        public TagObject Object { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Ref: {0}", Ref); }
        }
    }
}
