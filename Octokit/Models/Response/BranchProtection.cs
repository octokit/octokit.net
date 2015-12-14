using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Protection details for a <see cref="Branch"/>.
    /// Note: this is a PREVIEW api: https://developer.github.com/changes/2015-11-11-protected-branches-api/
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtection
    {
        /// <summary>
        /// Should this branch be protected or not
        /// </summary>
        public bool Enabled { get; protected set; }

        /// <summary>
        /// The <see cref="RequiredStatusChecks"/> information for this <see cref="Branch"/>.
        /// </summary>
        public RequiredStatusChecks RequiredStatusChecks { get; private set; }

        public BranchProtection(bool enabled, RequiredStatusChecks requiredStatusChecks)
        {
            Enabled = enabled;
            RequiredStatusChecks = requiredStatusChecks;
        }

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
        /// <summary>
        /// Who required status checks apply to
        /// </summary>
        public EnforcementLevel EnforcementLevel { get; protected set; }

        /// <summary>
        /// The list of status checks to require in order to merge into this <see cref="Branch"/>
        /// </summary>
        public ICollection<string> Contexts { get; private set; }

        public RequiredStatusChecks(EnforcementLevel enforcementLevel, ICollection<string> contexts)
        {
            EnforcementLevel = enforcementLevel;
            Contexts = contexts;
        }

        /// <summary>
        /// Adds the specified context to the required status checks.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        public void AddContext(string name)
        {
            // lazily create the contexts array
            if (Contexts == null)
            {
                Contexts = new List<string>();
            }

            Contexts.Add(name);
        }

        /// <summary>
        /// Clears all the contexts.
        /// </summary>
        public void ClearContexts()
        {
            // lazily create the contexts array
            if (Contexts == null)
            {
                Contexts = new List<string>();
            }
            else
            {
                Contexts.Clear();
            }
        }

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
