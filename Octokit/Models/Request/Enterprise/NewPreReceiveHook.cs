using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new pre-receive hook.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewPreReceiveHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewPreReceiveHook"/> class.
        /// </summary>
        /// <param name="name">The name of the hook.</param>
        /// <param name="script">The script that the hook runs.</param>
        /// <param name="scriptRepository">The repository where the script is kept.</param>
        /// <param name="environment">The pre-receive environment where the script is executed.</param>
        public NewPreReceiveHook(string name, Repository scriptRepository, string script, PreReceiveEnvironment environment)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(scriptRepository, nameof(scriptRepository));
            Ensure.ArgumentNotNullOrEmptyString(script, nameof(script));
            Ensure.ArgumentNotNull(environment, nameof(environment));

            Name = name;
            ScriptRepository = new RepositoryReference
            {
                FullName = scriptRepository.FullName
            };
            Script = script;
            Environment = new PreReceiveEnvironmentReference
            {
                Id = environment.Id
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewPreReceiveHook"/> class.
        /// </summary>
        /// <param name="name">The name of the hook.</param>
        /// <param name="script">The script that the hook runs.</param>
        /// <param name="scriptRepositoryFullName">The <see cref="Repository.FullName"/> of a repository where the script is kept.</param>
        /// <param name="environmentId">The <see cref="PreReceiveEnvironment.Id"/> of a pre-receive environment where the script is executed.</param>
        public NewPreReceiveHook(string name, string scriptRepositoryFullName, string script, long environmentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(script, nameof(script));
            Ensure.ArgumentNotNullOrEmptyString(scriptRepositoryFullName, nameof(scriptRepositoryFullName));

            Name = name;
            ScriptRepository = new RepositoryReference
            {
                FullName = scriptRepositoryFullName
            };
            Script = script;
            Environment = new PreReceiveEnvironmentReference
            {
                Id = environmentId
            };
        }

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
        /// The state of enforcement for this hook. default: <see cref="PreReceiveHookEnforcement.Disabled"/>
        /// </summary>
        public PreReceiveHookEnforcement? Enforcement { get; set; }

        /// <summary>
        /// Whether enforcement can be overridden at the org or repo level. default: false.
        /// </summary>
        public bool? AllowDownstreamConfiguration { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} Repo: {1} Script: {2}", Name, ScriptRepository.FullName, Script); }
        }
    }
}
