using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Protection details for a <see cref="Branch"/>.
    /// Note: this is a PREVIEW api: https://developer.github.com/changes/2015-11-11-protected-branches-api/
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtection
    {
        public BranchProtection() { }

        public BranchProtection(bool enabled, RequiredStatusChecks requiredStatusChecks)
        {
            Enabled = enabled;
            RequiredStatusChecks = requiredStatusChecks;
        }

        /// <summary>
        /// Should this branch be protected or not
        /// </summary>
        public bool Enabled { get; protected set; }

        /// <summary>
        /// The <see cref="RequiredStatusChecks"/> information for this <see cref="Branch"/>.
        /// </summary>
        public RequiredStatusChecks RequiredStatusChecks { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Enabled: {0}", Enabled);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RequiredStatusChecks
    {
        public RequiredStatusChecks() { }

        public RequiredStatusChecks(EnforcementLevel enforcementLevel, IEnumerable<string> contexts)
        {
            EnforcementLevel = enforcementLevel;
            Contexts = new ReadOnlyCollection<string>(contexts.ToList());
        }

        /// <summary>
        /// Who required status checks apply to
        /// </summary>
        public EnforcementLevel EnforcementLevel { get; protected set; }

        /// <summary>
        /// The list of status checks to require in order to merge into this <see cref="Branch"/>
        /// </summary>
        public IReadOnlyList<string> Contexts { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "EnforcementLevel: {0} Contexts: {1}", EnforcementLevel.ToString(), Contexts.Count);
            }
        }
    }

    /// <summary>
    /// The enforcement levels that are available
    /// </summary>
    public enum EnforcementLevel
    {
        /// <summary>
        /// Turn off required status checks for this <see cref="Branch"/>.
        /// </summary>
        Off,

        /// <summary>
        /// Required status checks will be enforeced for non-admins.
        /// </summary>
        NonAdmins,

        /// <summary>
        /// Required status checks will be enforced for everyone (including admins).
        /// </summary>
        Everyone
    }
}
