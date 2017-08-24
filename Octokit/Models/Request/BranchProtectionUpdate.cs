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
            RequiredPullRequestReviews = null;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories). Pass null to disable push access restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionPushRestrictionsUpdate restrictions)
        {
            RequiredStatusChecks = null;
            Restrictions = restrictions;
            RequiredPullRequestReviews = null;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredPullRequestReviews">Specifies if reviews are required to merge the pull request. Pass null to disable restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredReviewsUpdate requiredPullRequestReviews)
        {
            RequiredStatusChecks = null;
            Restrictions = null;
            RequiredPullRequestReviews = requiredPullRequestReviews;
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
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings. Pass null to disable status checks</param>
        /// <param name="requiredPullRequestReviews">Specifies if reviews are required to merge the pull request. Pass null to disable restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks, BranchProtectionRequiredReviewsUpdate requiredPullRequestReviews)
        {
            RequiredStatusChecks = requiredStatusChecks;
            RequiredPullRequestReviews = requiredPullRequestReviews;
            Restrictions = null;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories). Pass null to disable push access restrictions</param>
        /// <param name="requiredPullRequestReviews">Specifies if reviews are required to merge the pull request. Pass null to disable restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionPushRestrictionsUpdate restrictions, BranchProtectionRequiredReviewsUpdate requiredPullRequestReviews)
        {
            RequiredStatusChecks = null;
            RequiredPullRequestReviews = requiredPullRequestReviews;
            Restrictions = restrictions;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings. Pass null to disable status checks</param>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories). Pass null to disable push access restrictions</param>
        /// <param name="requiredPullRequestReviews">Specifies if reviews are required to merge the pull request. Pass null to disable restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks, BranchProtectionPushRestrictionsUpdate restrictions, BranchProtectionRequiredReviewsUpdate requiredPullRequestReviews)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = restrictions;
            RequiredPullRequestReviews = requiredPullRequestReviews;
        }

        /// <summary>
        /// Status check settings for the protected branch
        /// </summary>
        [SerializeNull]
        public BranchProtectionRequiredStatusChecksUpdate RequiredStatusChecks { get; protected set; }

        /// <summary>
        /// Pull Request review settings for the protected branch
        /// </summary>
        public BranchProtectionRequiredReviewsUpdate RequiredPullRequestReviews { get; protected set; }

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
                    "StatusChecks: {0} Restrictions: {1} PullRequestReviews: {2} EnforceAdmins: {3}",
                    RequiredStatusChecks == null ? "disabled" : RequiredStatusChecks.DebuggerDisplay,
                    Restrictions == null ? "disabled" : Restrictions.DebuggerDisplay,
                    RequiredPullRequestReviews == null ? "disabled" : RequiredPullRequestReviews.DebuggerDisplay);
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

    /// <summary>
    /// Specifies if pull request reviews are required before merging a pull request. Can optionally enforce the policy on repository administrators also.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRequiredReviewsUpdate
    {
        public BranchProtectionRequiredReviewsUpdate(bool includeAdmins)
        {
            IncludeAdmins = includeAdmins;
        }

        /// <summary>
        /// Enforce pull request reviews for repository administrators
        /// </summary>
        public bool IncludeAdmins { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "IncludeAdmins: {0}", IncludeAdmins);
            }
        }

    }
}
