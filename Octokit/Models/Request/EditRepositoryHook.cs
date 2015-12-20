using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents the requested changes to an edit repository hook.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EditRepositoryHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditRepositoryHook"/> class.
        /// </summary>
        public EditRepositoryHook() : this(null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditRepositoryHook"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public EditRepositoryHook(IDictionary<string, string> config)
        {
            Config = config;
        }

        public IDictionary<string, string> Config { get; private set; }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public IEnumerable<string> Events { get; set; }

        [Parameter(Key = "add_events")]
        public IEnumerable<string> AddEvents { get; set; }

        /// <summary>
        /// Gets or sets the remove events.
        /// </summary>
        /// <value>
        /// The remove events.
        /// </value>
        [Parameter(Key = "remove_events")]
        public IEnumerable<string> RemoveEvents { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>
        /// The active.
        /// </value>
        public bool? Active { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Replacing Events: {0}, Adding Events: {1}, Removing Events: {2}", Events == null ? "no" : string.Join(", ", Events),
                    AddEvents == null ? "no" : string.Join(", ", AddEvents),
                    RemoveEvents == null ? "no" : string.Join(", ", RemoveEvents));
            }
        }
    }
}