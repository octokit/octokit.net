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
        /// <param name="requiredStatusChecks">Specifies the requested status check settings. Pass null to disable status checks</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks)
        {
            RequiredStatusChecks = requiredStatusChecks;
            RequiredPullRequestReviews = null;
            Restrictions = null;
            EnforceAdmins = false;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredPullRequestReviews">Specifies if reviews are required to merge the pull request. Pass null to disable restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredReviewsUpdate requiredPullRequestReviews)
        {
            RequiredStatusChecks = null;
            RequiredPullRequestReviews = requiredPullRequestReviews;
            Restrictions = null;
            EnforceAdmins = false;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories). Pass null to disable push access restrictions</param>
        public BranchProtectionSettingsUpdate(BranchProtectionPushRestrictionsUpdate restrictions)
        {
            RequiredStatusChecks = null;
            RequiredPullRequestReviews = null;
            Restrictions = restrictions;
            EnforceAdmins = false;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="enforceAdmins">Specifies whether the protections applied to this branch also apply to repository admins</param>
        public BranchProtectionSettingsUpdate(bool enforceAdmins)
        {
            RequiredStatusChecks = null;
            RequiredPullRequestReviews = null;
            Restrictions = null;
            EnforceAdmins = enforceAdmins;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings. Pass null to disable status checks</param>
        /// <param name="requiredPullRequestReviews">Specifies if reviews are required to merge the pull request. Pass null to disable required reviews</param>
        /// <param name="enforceAdmins">Specifies whether the protections applied to this branch also apply to repository admins</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks, BranchProtectionRequiredReviewsUpdate requiredPullRequestReviews, bool enforceAdmins)
        {
            RequiredStatusChecks = requiredStatusChecks;
            RequiredPullRequestReviews = requiredPullRequestReviews;
            Restrictions = null;
            EnforceAdmins = enforceAdmins;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings. Pass null to disable status checks</param>
        /// <param name="requiredPullRequestReviews">Specifies if reviews are required to merge the pull request. Pass null to disable required reviews</param>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories). Pass null to disable push access restrictions</param>
        /// <param name="enforceAdmins">Specifies whether the protections applied to this branch also apply to repository admins</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks,
                                              BranchProtectionRequiredReviewsUpdate requiredPullRequestReviews,
                                              BranchProtectionPushRestrictionsUpdate restrictions,
                                              bool enforceAdmins)
        {
            RequiredStatusChecks = requiredStatusChecks;
            RequiredPullRequestReviews = requiredPullRequestReviews;
            Restrictions = restrictions;
            EnforceAdmins = enforceAdmins;
        }

        /// <summary>
        /// Create a BranchProtection update request
        /// </summary>
        /// <param name="requiredStatusChecks">Specifies the requested status check settings. Pass null to disable status checks</param>
        /// <param name="requiredPullRequestReviews">Specifies if reviews are required to merge the pull request. Pass null to disable required reviews</param>
        /// <param name="restrictions">Specifies the requested push access restrictions (applies only to Organization owned repositories). Pass null to disable push access restrictions</param>
        /// <param name="enforceAdmins">Specifies whether the protections applied to this branch also apply to repository admins</param>
        /// <param name="requiredLinearHistory">Enforces a linear commit Git history</param>
        /// <param name="allowForcePushes">Permits force pushes to the protected branch</param>
        /// <param name="allowDeletions">Allows deletion of the protected branch</param>
        /// <param name="blockCreations">The restrictions branch protection settings will also block pushes which create new branches</param>
        /// <param name="requiredConversationResolution">Requires all conversations on code to be resolved before a pull request can be merged</param>
        public BranchProtectionSettingsUpdate(BranchProtectionRequiredStatusChecksUpdate requiredStatusChecks,
                                              BranchProtectionRequiredReviewsUpdate requiredPullRequestReviews,
                                              BranchProtectionPushRestrictionsUpdate restrictions,
                                              bool enforceAdmins,
                                              bool requiredLinearHistory,
                                              bool? allowForcePushes,
                                              bool allowDeletions,
                                              bool blockCreations,
                                              bool requiredConversationResolution)
        {
            RequiredStatusChecks = requiredStatusChecks;
            RequiredPullRequestReviews = requiredPullRequestReviews;
            Restrictions = restrictions;
            EnforceAdmins = enforceAdmins;
            RequiredLinearHistory = requiredLinearHistory;
            AllowForcePushes = allowForcePushes;
            AllowDeletions = allowDeletions;
            BlockCreations = blockCreations;
            RequiredConversationResolution = requiredConversationResolution;
        }

        /// <summary>
        /// Status check settings for the protected branch
        /// </summary>
        [SerializeNull]
        public BranchProtectionRequiredStatusChecksUpdate RequiredStatusChecks { get; protected set; }

        /// <summary>
        /// Required Pull Request review settings for the protected branch
        /// </summary>
        [SerializeNull]
        public BranchProtectionRequiredReviewsUpdate RequiredPullRequestReviews { get; protected set; }

        /// <summary>
        /// Push access restrictions for the protected branch
        /// </summary>
        [SerializeNull]
        public BranchProtectionPushRestrictionsUpdate Restrictions { get; protected set; }

        /// <summary>
        /// Specifies whether the protections applied to this branch also apply to repository admins
        /// </summary>
        public bool EnforceAdmins { get; set; }

        /// <summary>
        /// Enforces a linear commit Git history. Default is false.
        /// </summary>
        public bool RequiredLinearHistory { get; set; }

        /// <summary>
        /// Permits force pushes to the protected branch by anyone with write access to the repository. Default is false.
        /// </summary>
        public bool? AllowForcePushes { get; set; }

        /// <summary>
        /// Allows deletion of the protected branch by anyone with write access to the repository. Default is false.
        /// </summary>
        public bool AllowDeletions { get; set; }

        /// <summary>
        /// If set to true, the restrictions branch protection settings which limits who can push will also block pushes which create new branches, unless the push is initiated by a user, team, or app which has the ability to push. Default is false.
        /// </summary>
        public bool BlockCreations { get; set; }

        /// <summary>
        /// Requires all conversations on code to be resolved before a pull request can be merged. Default is false.
        /// </summary>
        public bool RequiredConversationResolution { get; set; }


        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "RequiredStatusChecks: {0} RequiredPullRequestReviews: {1} Restrictions: {2} EnforceAdmins: {3}",
                    RequiredStatusChecks?.DebuggerDisplay ?? "disabled",
                    RequiredPullRequestReviews?.DebuggerDisplay ?? "disabled",
                    Restrictions?.DebuggerDisplay ?? "disabled",
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
        /// <param name="teams">List of Team slugs allowed to push to this branch</param>
        public BranchProtectionPushRestrictionsUpdate(BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNull(teams, nameof(teams));

            Teams = teams;
            Users = new BranchProtectionUserCollection();
        }

        /// <summary>
        /// Specify people (in addition to Administrators) allowed to push to this branch. Required status checks will still prevent these people from merging if the checks fail
        /// </summary>
        /// <param name="users">List of User logins allowed to push to this branch</param>
        public BranchProtectionPushRestrictionsUpdate(BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNull(users, nameof(users));

            Teams = new BranchProtectionTeamCollection();
            Users = users;
        }

        /// <summary>
        /// Specify teams and/or people (in addition to Administrators) allowed to push to this branch. Required status checks will still prevent these people from merging if the checks fail
        /// </summary>
        /// <param name="teams">List of Team slugs allowed to push to this branch</param>
        /// <param name="users">List of User logins allowed to push to this branch</param>
        public BranchProtectionPushRestrictionsUpdate(BranchProtectionTeamCollection teams, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNull(teams, nameof(teams));
            Ensure.ArgumentNotNull(users, nameof(users));

            Teams = teams;
            Users = users;
        }

        /// <summary>
        /// List of Team slugs allowed to push to this branch
        /// </summary>
        public BranchProtectionTeamCollection Teams { get; private set; }

        /// <summary>
        /// List of User logins allowed to push to this branch
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
    /// Specifies settings for requiring pull request reviews before merging a pull request.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRequiredReviewsUpdate
    {
        /// <summary>
        /// Settings for requiring reviews before a pull request can be merged.
        /// </summary>
        /// <param name="dismissStaleReviews">Dismiss approved reviews automatically when a new commit is pushed.</param>
        /// <param name="requireCodeOwnerReviews">Blocks merge until code owners have reviewed.</param>
        /// <param name="requiredApprovingReviewCount">Specify the number of reviewers required to approve pull requests. Use a number between 1 and 6.</param>
        public BranchProtectionRequiredReviewsUpdate(bool dismissStaleReviews, bool requireCodeOwnerReviews, int requiredApprovingReviewCount)
        {
            DismissStaleReviews = dismissStaleReviews;
            RequireCodeOwnerReviews = requireCodeOwnerReviews;
            RequiredApprovingReviewCount = requiredApprovingReviewCount;
        }

        /// <summary>
        /// Settings for requiring reviews before a pull request can be merged.
        /// </summary>
        /// <param name="dismissalRestrictions">Specify which users and teams can dismiss pull request reviews (applies only to Organization owned repositories).</param>
        /// <param name="dismissStaleReviews">Dismiss approved reviews automatically when a new commit is pushed.</param>
        /// <param name="requireCodeOwnerReviews">Blocks merge until code owners have reviewed.</param>
        /// <param name="requiredApprovingReviewCount">Specify the number of reviewers required to approve pull requests. Use a number between 1 and 6.</param>
        public BranchProtectionRequiredReviewsUpdate(BranchProtectionRequiredReviewsDismissalRestrictionsUpdate dismissalRestrictions, bool dismissStaleReviews, bool requireCodeOwnerReviews, int requiredApprovingReviewCount)
        {
            Ensure.ArgumentNotNull(dismissalRestrictions, nameof(dismissalRestrictions));

            DismissalRestrictions = dismissalRestrictions;
            DismissStaleReviews = dismissStaleReviews;
            RequireCodeOwnerReviews = requireCodeOwnerReviews;
            RequiredApprovingReviewCount = requiredApprovingReviewCount;
        }

        /// <summary>
        /// Specify which users and teams can dismiss pull request reviews.
        /// </summary>
        public BranchProtectionRequiredReviewsDismissalRestrictionsUpdate DismissalRestrictions { get; protected set; }

        /// <summary>
        /// Dismiss approved reviews automatically when a new commit is pushed.
        /// </summary>
        public bool DismissStaleReviews { get; protected set; }

        /// <summary>
        /// Blocks merge until code owners have reviewed.
        /// </summary>
        public bool RequireCodeOwnerReviews { get; protected set; }

        /// <summary>
        /// Specify the number of reviewers required to approve pull requests. Use a number between 1 and 6.
        /// </summary>
        public int RequiredApprovingReviewCount { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "DismissalRestrictions: {0} DismissStaleReviews: {1} RequireCodeOwnerReviews: {2} RequiredApprovingReviewCount: {3}",
                    DismissalRestrictions?.DebuggerDisplay ?? "disabled",
                    DismissStaleReviews,
                    RequireCodeOwnerReviews,
                    RequiredApprovingReviewCount);
            }
        }
    }

    /// <summary>
    /// Specifies whether review dismissal for the protected branch will be restricted to Admins, specified Teams/Users or unrestricted
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRequiredReviewsDismissalRestrictionsUpdate
    {
        /// <summary>
        /// Specify whether dismissing reviews is restricted or not
        /// </summary>
        /// <param name="enabled">True to restrict review dismissal to Administrators, false to disable restrictions</param>
        public BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(bool enabled)
        {
            if (enabled)
            {
                // Empty Teams/Users list means restrictions are enabled with only Admins being able to dismiss reviews
                Teams = new BranchProtectionTeamCollection();
                Users = new BranchProtectionUserCollection();
            }
            else
            {
                // To disable the review dismissal restriction, the API requires an object with empty members to be passed
                Teams = null;
                Users = null;
            }
        }

        /// <summary>
        /// Restrict dismissing reviews to the specified teams (in addition to Administrators).
        /// </summary>
        /// <param name="teams">List of Team slugs allowed to dismiss reviews</param>
        public BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNull(teams, nameof(teams));

            Teams = teams;
            Users = new BranchProtectionUserCollection();
        }

        /// <summary>
        /// Restrict dismissing reviews to the specified people (in addition to Administrators).
        /// </summary>
        /// <param name="users">List of User logins allowed to dismiss reviews</param>
        public BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNull(users, nameof(users));

            Teams = new BranchProtectionTeamCollection();
            Users = users;
        }

        /// <summary>
        /// Restrict dismissing reviews to the specified teams and people (in addition to Administrators).
        /// </summary>
        /// <param name="teams">List of Team slugs allowed to dismiss reviews</param>
        /// <param name="users">List of User logins allowed to dismiss reviews</param>
        public BranchProtectionRequiredReviewsDismissalRestrictionsUpdate(BranchProtectionTeamCollection teams, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNull(teams, nameof(teams));
            Ensure.ArgumentNotNull(users, nameof(users));

            Teams = teams;
            Users = users;
        }

        /// <summary>
        /// List of Team slugs allowed to dismiss reviews
        /// </summary>
        public BranchProtectionTeamCollection Teams { get; private set; }

        /// <summary>
        /// List of User logins allowed to dismiss reviews
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
}
