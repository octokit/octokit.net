using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a pre-receive hook.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PreReceiveHook
    {
        public PreReceiveHook()
        { }

        public PreReceiveHook(int id, string name, StringEnum<PreReceiveHookEnforcement> enforcement, string script, Repository scriptRepository, PreReceiveEnvironment environment, bool allowDownstreamConfiguration)
        {
            Id = id;
            Name = name;
            Enforcement = enforcement;
            Script = script;
            ScriptRepository = scriptRepository;
            Environment = environment;
            AllowDownstreamConfiguration = allowDownstreamConfiguration;
        }

        /// <summary>
        /// The identifier for the pre-receive hook.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The name of the hook.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The state of enforcement for this hook.
        /// </summary>
        public StringEnum<PreReceiveHookEnforcement> Enforcement { get; private set; }

        /// <summary>
        /// The script that the hook runs.
        /// </summary>
        public string Script { get; private set; }

        /// <summary>
        /// The GitHub repository where the script is kept.
        /// </summary>
        public Repository ScriptRepository { get; private set; }

        /// <summary>
        /// The pre-receive environment where the script is executed.
        /// </summary>
        public PreReceiveEnvironment Environment { get; private set; }

        /// <summary>
        /// Whether enforcement can be overridden at the org or repo level.
        /// </summary>
        public bool AllowDownstreamConfiguration { get; private set; }

        public UpdatePreReceiveHook ToUpdate()
        {
            return new UpdatePreReceiveHook
            {
                Name = Name,
                Enforcement = Enforcement.Value,
                Script = Script,
                ScriptRepository = new RepositoryReference
                {
                    FullName = ScriptRepository.FullName
                },
                Environment = new PreReceiveEnvironmentReference
                {
                    Id = Environment.Id
                },
                AllowDownstreamConfiguration = AllowDownstreamConfiguration
            };
        }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1} Script: {2}", Id, Name, Script); }
        }
    }
}
