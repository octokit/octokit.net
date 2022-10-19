using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new pre-receive hook.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdatePreReceiveHook
    {
        /// <summary>
        /// The name of the hook.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The script that the hook runs.
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// The script that the hook runs.
        /// </summary>
        public RepositoryReference ScriptRepository { get; set; }

        /// <summary>
        /// The script that the hook runs.
        /// </summary>
        public PreReceiveEnvironmentReference Environment { get; set; }

        /// <summary>
        /// The state of enforcement for this hook. Defaults is <see cref="PreReceiveHookEnforcement.Disabled"/>
        /// </summary>
        public PreReceiveHookEnforcement? Enforcement { get; set; }

        /// <summary>
        /// Whether enforcement can be overridden at the org or repo level. Default is false.
        /// </summary>
        public bool? AllowDownstreamConfiguration { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} Script: {1}", Name, Script); }
        }
    }
}
