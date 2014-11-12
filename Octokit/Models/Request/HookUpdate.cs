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
    public class HookUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HookUpdate"/> class.
        /// </summary>
        public HookUpdate()
        {
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
        /// Determines whether the hook is actually triggered on pushes
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Determines what events the hook is triggered for. 
        /// </summary>
        public IEnumerable<string> Events { get; set; }

        /// <summary>
        /// Determines a list of events to be added to the list of events that the Hook triggers for.
        /// </summary>
        public IEnumerable<string> AddEvents { get; set; }

        /// <summary>
        /// Determines a list of events to be removed from the list of events that the Hook triggers for.
        /// </summary>
        public IEnumerable<string> RemoveEvents { get; set; }
        
        /// <summary>
        /// The configuration for this hook.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IDictionary<string, object> Config { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Active: {0} Events: {1}", Active, string.Join(", ", Events));
            }
        }
    }
}