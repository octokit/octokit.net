using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Specifies the requested settings for branch protection
    /// </summary>
    /// <remarks>
    /// Note: this is a PREVIEW api: https://developer.github.com/changes/2016-06-27-protected-branches-api-update/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionSettingsUpdate
    {
        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="enforceAdmins">Specifies whether the protections applied to this branch also apply to repository admins</param>
        public BranchProtectionSettingsUpdate(bool enforceAdmins)
        {
            EnforceAdmins = enforceAdmins;
            RequiredStatusChecks = null;
            Restrictions = null;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings. Pass null to disable status checks</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = null;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories). Pass null to disable push access restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionPushRestrictionsUpdate restrictions)
        {
            RequiredStatusChecks = null;
            Restrictions = restrictions;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings. Pass null to disable status checks</param>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories). Pass null to disable push access restrictions</param>
        /// <param name="enforceAdmins">Specifies whether the protections applied to this branch also apply to repository admins</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks, BranchProtectionPushRestrictionsUpdate restrictions, bool enforceAdmins)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = restrictions;
            EnforceAdmins = enforceAdmins;
        }

        /// <summary>
        /// Specifies whether the protections applied to this branch also apply to repository admins
        /// </summary>
        public bool EnforceAdmins { get; set; }

        /// <summary>
        /// Status check settings for the protected branch
        /// </summary>
        [SerializeNull]
        public BranchProtectionRequiredStatusChecksUpdate RequiredStatusChecks { get; protected set; }

        /// <summary>
        /// Push access restrictions for the protected branch
        /// </summary>
        [SerializeNull]
        public BranchProtectionPushRestrictionsUpdate Restrictions { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "StatusChecks: {0} Restrictions: {1} EnforceAdmins: {2}",
                    RequiredStatusChecks == null ? "disabled" : RequiredStatusChecks.DebuggerDisplay,
                    Restrictions == null ? "disabled" : Restrictions.DebuggerDisplay,
                    EnforceAdmins);
            }
        }
    }

    /// <summary>
    /// Specifies settings for status checks which must pass before branches can be merged into the protected branch
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRequiredStatusChecksUpdate
    {
        /// <summary>
        /// Status check settings for branch protection
        /// </summary>
        /// <param name="strict">Require branches to be up to date before merging</param>
        /// <param name="contexts">Require status checks to pass before merging</param>
        public BranchProtectionRequiredStatusChecksUpdate(bool strict, IReadOnlyList<string> contexts)
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
                return string.Format(CultureInfo.InvariantCulture, "Strict: {0} Contexts: {1}", Strict, Contexts == null ? "" : string.Join(",", Contexts));
            }
        }
    }

    /// <summary>
    /// Specifies teams and/or people allowed to push to the protected branch. Required status checks will still prevent these people from merging if the checks fail
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionPushRestrictionsUpdate
    {
        /// <summary>
        /// Specify only administrators are allowed to push to this branch. Required status checks will still prevent these people from merging if the checks fail
        /// </summary>
        public BranchProtectionPushRestrictionsUpdate()
        {
            Teams = new BranchProtectionTeamCollection();
            Users = new BranchProtectionUserCollection();
        }

        /// <summary>
        /// Specify teams (in addition to Administrators) allowed to push to this branch. Required status checks will still prevent these people from merging if the checks fail
        /// </summary>
        /// <param name="teams">Teams allowed to push to this branch</param>
        public BranchProtectionPushRestrictionsUpdate(BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNull(teams, "teams");

            Teams = teams;
            Users = new BranchProtectionUserCollection();
        }

        /// <summary>
        /// Specify people (in addition to Administrators) allowed to push to this branch. Required status checks will still prevent these people from merging if the checks fail
        /// </summary>
        /// <param name="users">Users allowed to push to this branch</param>
        public BranchProtectionPushRestrictionsUpdate(BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNull(users, "users");

            Teams = new BranchProtectionTeamCollection();
            Users = users;
        }

        /// <summary>
        /// Specify teams and/or people (in addition to Administrators) allowed to push to this branch. Required status checks will still prevent these people from merging if the checks fail
        /// </summary>
        /// <param name="teams">Teams allowed to push to this branch</param>
        /// <param name="users">Users allowed to push to this branch</param>
        public BranchProtectionPushRestrictionsUpdate(BranchProtectionTeamCollection teams, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNull(teams, "teams");
            Ensure.ArgumentNotNull(users, "users");

            Teams = teams;
            Users = users;
        }

        /// <summary>
        /// Teams allowed to push to this branch
        /// </summary>
        public BranchProtectionTeamCollection Teams { get; private set; }

        /// <summary>
        /// Users allowed to push to this branch
        /// </summary>
        public BranchProtectionUserCollection Users { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Teams: {0} Users: {1}",
                    Teams == null ? "" : Teams.DebuggerDisplay,
                    Users == null ? "" : Users.DebuggerDisplay);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionTeamCollection : Collection<string>
    {
        public BranchProtectionTeamCollection()
        { }

        public BranchProtectionTeamCollection(IList<string> list) : base(list)
        { }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, String.Join(", ", this));
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionUserCollection : Collection<string>
    {
        public BranchProtectionUserCollection()
        { }

        public BranchProtectionUserCollection(IList<string> list) : base(list)
        { }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, String.Join(", ", this));
            }
        }
    }
}
