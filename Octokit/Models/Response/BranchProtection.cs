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
    [Obsolete("BranchProtection preview functionality in the GitHub API has had breaking changes.  This existing implementation will cease to work when the preview period ends.")]
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
    [Obsolete("BranchProtection preview functionality in the GitHub API has had breaking changes.  This existing implementation will cease to work when the preview period ends.")]
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
    [Obsolete("BranchProtection preview functionality in the GitHub API has had breaking changes.  This existing implementation will cease to work when the preview period ends.")]
    public enum EnforcementLevel
    {
        /// <summary>
        /// Turn off required status checks for this <see cref="Branch"/>.
        /// </summary>
        Off,

        /// <summary>
        /// Required status checks will be enforced for non-admins.
        /// </summary>
        NonAdmins,

        /// <summary>
        /// Required status checks will be enforced for everyone (including admins).
        /// </summary>
        Everyone
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionSettings
    {
        public BranchProtectionSettings() { }

        public BranchProtectionSettings(BranchProtectionRequiredStatusChecks requiredStatusChecks, ProtectedBranchRestrictions restrictions)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = restrictions;
        }

        public BranchProtectionRequiredStatusChecks RequiredStatusChecks { get; protected set; }
        public ProtectedBranchRestrictions Restrictions { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "StatusChecks: {0} Restrictions: {1}", RequiredStatusChecks, Restrictions);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionSettingsUpdate
    {
        public BranchProtectionSettingsUpdate() { }

        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecks requiredStatusChecks, ProtectedBranchRestrictionsUpdate restrictions)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = restrictions;
        }

        public BranchProtectionRequiredStatusChecks RequiredStatusChecks { get; protected set; }

        public ProtectedBranchRestrictionsUpdate Restrictions { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "StatusChecks: {0} Restrictions: {1}", RequiredStatusChecks, Restrictions);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRequiredStatusChecks
    {
        public BranchProtectionRequiredStatusChecks() { }

        public BranchProtectionRequiredStatusChecks(bool includeAdmins, bool strict, IReadOnlyList<string> contexts)
        {
            IncludeAdmins = includeAdmins;
            Strict = strict;
            Contexts = contexts;
        }

        public bool IncludeAdmins { get; protected set; }

        public bool Strict { get; protected set; }

        public IReadOnlyList<string> Contexts { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "IncludeAdmins: {0} Strict: {1} Contexts: {2}", IncludeAdmins, Strict, String.Join(",", Contexts));
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProtectedBranchRestrictions
    {
        public ProtectedBranchRestrictions() { }

        public ProtectedBranchRestrictions(IReadOnlyList<Team> teams, IReadOnlyList<User> users)
        {
            Teams = teams;
            Users = users;
        }

        public IReadOnlyList<Team> Teams { get; private set; }

        public IReadOnlyList<User> Users { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Teams: {0} Users: {1}", Teams, Users);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProtectedBranchRestrictionsUpdate
    {
        public ProtectedBranchRestrictionsUpdate() { }

        public ProtectedBranchRestrictionsUpdate(IReadOnlyList<string> teams, IReadOnlyList<string> users)
        {
            Teams = teams;
            Users = users;
        }

        public IReadOnlyList<string> Teams { get; private set; }

        public IReadOnlyList<string> Users { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Teams: {0} Users: {1}", String.Join(",", Teams), String.Join(",", Users));
            }
        }
    }
}
