using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// An update to an existing hook.
    /// </summary>
    /// <remarks>https://developer.github.com/v3/repos/hooks/#edit-a-hook</remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class HookUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HookUpdate"/> class.
        /// </summary>
        public HookUpdate()
        {
            Events = new List<string>();
            AddEvents = new List<string>();
            RemoveEvents = new List<string>();
            Config = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HookUpdate" /> class.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        public HookUpdate(bool active) : this()
        {
            Active = active;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HookUpdate" /> class.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="events">The events.</param>
        /// <param name="config">The configuration.</param>
        public HookUpdate(bool active, ICollection<string> events, IDictionary<string, object> config) : this()
        {
            Active = active;
            Events = events;
            Config = config;
        }

        /// <summary>
        /// Determines whether the hook is actually triggered on pushes
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Determines what events the hook is triggered for. 
        /// </summary>
        public ICollection<string> Events { get; private set; }

        /// <summary>
        /// Determines a list of events to be added to the list of events that the Hook triggers for.
        /// </summary>
        public ICollection<string> AddEvents { get; private set; }

        /// <summary>
        /// Determines a list of events to be removed from the list of events that the Hook triggers for.
        /// </summary>
        public ICollection<string> RemoveEvents { get; private set; }
        
        /// <summary>
        /// The configuration for this hook.
        /// </summary>
        public IDictionary<string, object> Config { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Active: {0} Events: {1}", Active, string.Join(", ", Events));
            }
        }
    }
}