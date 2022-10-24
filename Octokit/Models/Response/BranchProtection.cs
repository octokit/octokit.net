using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Protection details for a <see cref="Branch"/>.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionSettings
    {
        public BranchProtectionSettings() { }

        public BranchProtectionSettings(BranchProtectionRequiredStatusChecks requiredStatusChecks,
                                        BranchProtectionRequiredReviews requiredPullRequestReviews,
                                        BranchProtectionPushRestrictions restrictions,
                                        EnforceAdmins enforceAdmins,
                                        BranchProtectionEnabledCommon requiredLinearHistory,
                                        BranchProtectionEnabledCommon allowForcePushes,
                                        BranchProtectionEnabledCommon allowDeletions,
                                        BranchProtectionEnabledCommon blockCreations,
                                        BranchProtectionEnabledCommon requiredConversationResolution,
                                        BranchProtectionEnabledCommon requiredSignatures)
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
            RequiredSignatures = requiredSignatures;
        }



        /// <summary>
        /// Status check settings for the protected branch
        /// </summary>
        public BranchProtectionRequiredStatusChecks RequiredStatusChecks { get; private set; }

        /// <summary>
        /// Required review settings for the protected branch
        /// </summary>
        public BranchProtectionRequiredReviews RequiredPullRequestReviews { get; private set; }

        /// <summary>
        /// Push access restrictions for the protected branch
        /// </summary>
        public BranchProtectionPushRestrictions Restrictions { get; private set; }

        /// <summary>
        /// Specifies whether the protections applied to this branch also apply to repository admins
        /// </summary>
        public EnforceAdmins EnforceAdmins { get; private set; }

        /// <summary>
        /// Specifies whether a linear history is required
        /// </summary>
        public BranchProtectionEnabledCommon RequiredLinearHistory { get; private set; }

        /// <summary>
        /// Specifies whether force pushes are allowed
        /// </summary>
        public BranchProtectionEnabledCommon AllowForcePushes { get; private set; }

        /// <summary>
        /// Specifies whether deletions are allowed
        /// </summary>
        public BranchProtectionEnabledCommon AllowDeletions { get; private set; }

        /// <summary>
        /// Specifies whether creations can be blocked
        /// </summary>
        public BranchProtectionEnabledCommon BlockCreations { get; private set; }

        /// <summary>
        /// Specifies whether conversation resolution iss required
        /// </summary>
        public BranchProtectionEnabledCommon RequiredConversationResolution { get; private set; }

        /// <summary>
        /// Specifies whether signatures are required
        /// </summary>
        public BranchProtectionEnabledCommon RequiredSignatures { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "RequiredStatusChecks: {0} RequiredPullRequestReviews {1} Restrictions: {2} EnforceAdmins: {3}",
                    RequiredStatusChecks?.DebuggerDisplay ?? "disabled",
                    RequiredPullRequestReviews?.DebuggerDisplay ?? "disabled",
                    Restrictions?.DebuggerDisplay ?? "disabled",
                    EnforceAdmins?.DebuggerDisplay ?? "disabled");
            }
        }
    }

    /// <summary>
    /// Specifies whether the protections applied to this branch also apply to repository admins
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnforceAdmins
    {
        public EnforceAdmins() { }

        public EnforceAdmins(bool enabled)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; private set; }

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
        public bool Strict { get; private set; }

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
                    Teams == null ? "" : String.Join(",", Teams.Select(x => x.Name)),
                    Users == null ? "" : String.Join(",", Users.Select(x => x.Login)));
            }
        }
    }

    /// <summary>
    /// Specifies if pull request reviews are required before merging a pull request. Can optionally enforce the policy on repository administrators also.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRequiredReviews
    {
        public BranchProtectionRequiredReviews() { }

        public BranchProtectionRequiredReviews(BranchProtectionRequiredReviewsDismissalRestrictions dismissalRestrictions, bool dismissStaleReviews, bool requireCodeOwnerReviews, int requiredApprovingReviewCount)
        {
            DismissalRestrictions = dismissalRestrictions;
            DismissStaleReviews = dismissStaleReviews;
            RequireCodeOwnerReviews = requireCodeOwnerReviews;
            RequiredApprovingReviewCount = requiredApprovingReviewCount;
        }

        /// <summary>
        /// Specify which users and teams can dismiss pull request reviews.
        /// </summary>
        public BranchProtectionRequiredReviewsDismissalRestrictions DismissalRestrictions { get; private set; }

        /// <summary>
        /// Dismiss approved reviews automatically when a new commit is pushed.
        /// </summary>
        public bool DismissStaleReviews { get; private set; }

        /// <summary>
        /// Blocks merge until code owners have reviewed.
        /// </summary>
        public bool RequireCodeOwnerReviews { get; private set; }

        /// <summary>
        /// Specify the number of reviewers required to approve pull requests. Use a number between 1 and 6.
        /// </summary>
        public int RequiredApprovingReviewCount { get; private set; }

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
    /// Specifies people or teams allowed to push to the protected branch. Required status checks will still prevent these people from merging if the checks fail
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRequiredReviewsDismissalRestrictions
    {
        public BranchProtectionRequiredReviewsDismissalRestrictions() { }

        public BranchProtectionRequiredReviewsDismissalRestrictions(IReadOnlyList<Team> teams, IReadOnlyList<User> users)
        {
            Teams = teams;
            Users = users;
        }

        /// <summary>
        /// The specified Teams that can dismiss reviews
        /// </summary>
        public IReadOnlyList<Team> Teams { get; private set; }

        /// <summary>
        /// The specified Users who can dismiss reviews
        /// </summary>
        public IReadOnlyList<User> Users { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Teams: {0} Users: {1}",
                    Teams == null ? "" : String.Join(",", Teams.Select(x => x.Name)),
                    Users == null ? "" : String.Join(",", Users.Select(x => x.Login)));
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionEnabledCommon
    {
        public BranchProtectionEnabledCommon() { }

        public BranchProtectionEnabledCommon(bool enabled)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Enabled: {0}", Enabled);
            }
        }
    }
}
