using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Specifies the values used to create a workflow dispatch event.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CreateWorkflowDispatch
    {
        /// <summary>
        /// Creates a new workflow dispatch event.
        /// </summary>
        /// <param name="ref">Required. The git reference for the workflow. The reference can be a branch or tag name.</param>
        public CreateWorkflowDispatch(string @ref)
        {
            Ensure.ArgumentNotNullOrEmptyString(@ref, nameof(@ref));
            Ref = @ref;
        }

        /// <summary>
        /// The git reference for the workflow. The reference can be a branch or tag name.
        /// </summary>
        public string Ref { get; private set; }

        /// <summary>
        /// Input keys and values configured in the workflow file.
        /// </summary>
        public IDictionary<string, object> Inputs { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Ref: {0}", Ref);
            }
        }
    }
}
