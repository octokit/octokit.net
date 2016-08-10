using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Specifies the requested settings for branch protection
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionSettingsUpdate
    {
        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = null;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings.  Pass null to disable status checks</param>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories).  Pass null to disable push access restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks, ProtectedBranchRestrictionsUpdate restrictions)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = restrictions;
        }

        /// <summary>
        /// Status check settings for protected branch
        /// </summary>
        [SerializeNull]
        public BranchProtectionRequiredStatusChecksUpdate RequiredStatusChecks { get; protected set; }

        /// <summary>
        /// Push access restrictions for the protected branch
        /// </summary>
        [SerializeNull]
        public ProtectedBranchRestrictionsUpdate Restrictions { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "StatusChecks: {0} Restrictions: {1}", RequiredStatusChecks.DebuggerDisplay, Restrictions.DebuggerDisplay);
            }
        }
    }

    /// <summary>
    /// Specifies the requested status check settings for branch protection
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRequiredStatusChecksUpdate
    {
        /// <summary>
        /// Status check settings for branch protection
        /// </summary>
        /// <param name="includeAdmins">Enforce required status checks for repository administrators</param>
        /// <param name="strict">Require branches to be up to date before merging</param>
        /// <param name="contexts">Require status checks to pass before merging</param>
        public BranchProtectionRequiredStatusChecksUpdate(bool includeAdmins, bool strict, IReadOnlyList<string> contexts)
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
                return String.Format(CultureInfo.InvariantCulture, "IncludeAdmins: {0} Strict: {1} Contexts: {2}", IncludeAdmins, Strict, Contexts == null ? "" : String.Join(",", Contexts));
            }
        }
    }

    /// <summary>
    /// Specifies people or teams allowed to push to this branch. Required status checks will still prevent these people from merging if the checks fail.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProtectedBranchRestrictionsUpdate
    {
        /// <summary>
        /// Specify people or teams (in addition to Administrators) allowed to push to this branch. Required status checks will still prevent these people from merging if the checks fail.
        /// </summary>
        /// <param name="teams">Teams allowed to push to this branch (pass empty array if no teams)</param>
        /// <param name="users">Users allowed to push to this branch (pass empty array if no users)</param>
        public ProtectedBranchRestrictionsUpdate(IReadOnlyList<string> teams, IReadOnlyList<string> users)
        {
            Ensure.ArgumentNotNull(teams, "teams");
            Ensure.ArgumentNotNull(users, "users");

            Teams = teams;
            Users = users;
        }

        /// <summary>
        /// Teams allowed to push to this branch
        /// </summary>
        public IReadOnlyList<string> Teams { get; private set; }

        /// <summary>
        /// Users allowed to push to this branch
        /// </summary>
        public IReadOnlyList<string> Users { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Teams: {0} Users: {1}", Teams == null ? "" : String.Join(",", Teams), Users == null ? "" : String.Join(",", Users));
            }
        }
    }
}
