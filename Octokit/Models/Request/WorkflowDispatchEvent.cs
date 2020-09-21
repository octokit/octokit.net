using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents workflow dispatch event. Values that are null will not be sent in the request.
    /// Use string.empty if you want to clear a value.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowDispatchEvent
    {
        /// <summary>
        /// The reference of the workflow run. The reference can be a branch, tag, or a commit SHA.
        /// </summary>
        public string Ref { get; set; }
        
        /// <summary>
        /// Input keys and values configured in the workflow file. The maximum number of properties is 10.
        /// Default: Any default properties configured in the workflow file will be used when `inputs` are omitted.
        /// </summary>
        public object Inputs { get; set; }
        
        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Ref: {0}", Ref);
    }
}