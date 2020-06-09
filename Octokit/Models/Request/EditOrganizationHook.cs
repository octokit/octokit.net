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
    public class EditOrganizationHook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditOrganizationHook"/> class.
        /// </summary>
        public EditOrganizationHook() : this(null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditOrganizationHook"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public EditOrganizationHook(IDictionary<string, string> config)
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
                    "Organizaton Hook: Events: {0}", Events == null ? "no" : string.Join(", ", Events));
            }
        }
    }
}