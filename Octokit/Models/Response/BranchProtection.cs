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
    /// </summary>
    /// <remarks>
    /// Note: this is a PREVIEW api: https://developer.github.com/changes/2015-11-11-protected-branches-api/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please see BranchProtectionSettings instead.")]
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
                return string.Format(CultureInfo.InvariantCulture, "Enabled: {0}", Enabled);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please see BranchProtectionRequiredStatusChecks instead.")]
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
                return string.Format(CultureInfo.InvariantCulture, "EnforcementLevel: {0} Contexts: {1}", EnforcementLevel.ToString(), Contexts.Count);
            }
        }
    }

    /// <summary>
    /// The enforcement levels that are available
    /// </summary>
    [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please see BranchProtectionRequiredStatusChecks.IncludeAdmins instead.")]
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

    /// <summary>
    /// Protection details for a <see cref="Branch"/>.
    /// </summary>
    /// <remarks>
    /// Note: this is a PREVIEW api: https://developer.github.com/changes/2016-06-27-protected-branches-api-update/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionSettings
    {
        public BranchProtectionSettings() { }

        public BranchProtectionSettings(BranchProtectionRequiredStatusChecks requiredStatusChecks, BranchProtectionPushRestrictions restrictions)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = restrictions;
        }

        /// <summary>
        /// Status check settings for the protected branch
        /// </summary>
        public BranchProtectionRequiredStatusChecks RequiredStatusChecks { get; protected set; }

        /// <summary>
        /// Push access restrictions for the protected branch
        /// </summary>
        public BranchProtectionPushRestrictions Restrictions { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "StatusChecks: {0} Restrictions: {1}",
                    RequiredStatusChecks == null ? "disabled" : RequiredStatusChecks.DebuggerDisplay,
                    Restrictions == null ? "disabled" : Restrictions.DebuggerDisplay);
            }
        }
    }

    /// <summary>
    /// Specifies settings for status checks which must pass before branches can be merged into the protected branch
    /// </summary>
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

        /// <summary>
        /// Enforce required status checks for repository administrators
        /// </summary>
        public bool IncludeAdmins { get; protected set; }

        /// <summary>
        /// Require branches to be up to date before merging
        /// </summary>
        public bool Strict { get; protected set; }

        /// <summary>
        /// Require status checks to pass before merging
        /// </summary>
        public IReadOnlyList<string> Contexts { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "IncludeAdmins: {0} Strict: {1} Contexts: {2}",
                    IncludeAdmins, 
                    Strict, 
                    Contexts == null ? "" : String.Join(",", Contexts));
            }
        }
    }

    /// <summary>
    /// Specifies people or teams allowed to push to the protected branch. Required status checks will still prevent these people from merging if the checks fail
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionPushRestrictions
    {
        public BranchProtectionPushRestrictions() { }

        public BranchProtectionPushRestrictions(IReadOnlyList<Team> teams, IReadOnlyList<User> users)
        {
            Teams = teams;
            Users = users;
        }

        /// <summary>
        /// Push access is restricted to the specified Teams
        /// </summary>
        public IReadOnlyList<Team> Teams { get; private set; }

        /// <summary>
        /// Push access is restricted to the specified Users
        /// </summary>
        public IReadOnlyList<User> Users { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "Teams: {0} Users: {1}",
                    Teams == null ? "" : String.Join(",", Teams),
                    Users == null ? "" : String.Join(",", Users));
            }
        }
    }
}
