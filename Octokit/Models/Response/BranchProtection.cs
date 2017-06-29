using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Octokit.Internal;

namespace Octokit
{
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

        /// <summary>
        /// Specifies whether the protections applied to this branch also apply to repository admins
        /// </summary>
        public EnforceAdmins EnforceAdmins { get; protected set; }
    }

    /// <summary>
    /// Specifies whether the protections applied to this branch also apply to repository admins
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnforceAdmins
    {
        public bool Enabled { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Enabled: {0}", Enabled);
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

        public BranchProtectionRequiredStatusChecks(bool strict, IReadOnlyList<string> contexts)
        {
            Strict = strict;
            Contexts = contexts;
        }

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
                    "Strict: {0} Contexts: {1}",
                    Strict,
                    Contexts == null ? "" : string.Join(",", Contexts));
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
