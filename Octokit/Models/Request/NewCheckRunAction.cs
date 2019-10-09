using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckRunAction
    {
        /// <summary>
        /// Constructs a CheckRunAction request object
        /// </summary>
        /// <param name="label">Required. The text to be displayed on a button in the web UI. The maximum size is 20 characters</param>
        /// <param name="description">Required. A short explanation of what this action would do. The maximum size is 40 characters</param>
        /// <param name="identifier">Required. A reference for the action on the integrator's system. The maximum size is 20 characters</param>
        public NewCheckRunAction(string label, string description, string identifier)
        {
            Label = label;
            Description = description;
            Identifier = identifier;
        }

        /// <summary>
        /// Required. The text to be displayed on a button in the web UI. The maximum size is 20 characters
        /// </summary>
        public string Label { get; protected set; }

        /// <summary>
        /// Required. A short explanation of what this action would do. The maximum size is 40 characters
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Required. A reference for the action on the integrator's system. The maximum size is 20 characters
        /// </summary>
        public string Identifier { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "Label: {0}", Label);
    }
}