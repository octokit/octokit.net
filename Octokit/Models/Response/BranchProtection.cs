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
    /// <remarks>
    /// Note: this is a PREVIEW api: https://developer.github.com/changes/2016-06-27-protected-branches-api-update/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionSettings
    {
        public BranchProtectionSettings() { }

        public BranchProtectionSettings(BranchProtectionRequiredStatusChecks requiredStatusChecks, BranchProtectionPushRestrictions restrictions, BranchProtectionRequiredReviews requiredPullRequestReviews, EnforceAdmins enforceAdmins)
        {
            RequiredStatusChecks = requiredStatusChecks;
            Restrictions = restrictions;
            RequiredPullRequestReviews = requiredPullRequestReviews;
            EnforceAdmins = enforceAdmins;
        }

        /// <summary>
        /// Status check settings for the protected branch
        /// </summary>
        public BranchProtectionRequiredStatusChecks RequiredStatusChecks { get; protected set; }

        /// <summary>
        /// Required review settings for the protected branch
        /// </summary>
        public BranchProtectionRequiredReviews RequiredPullRequestReviews { get; protected set; }

        /// <summary>
        /// Push access restrictions for the protected branch
        /// </summary>
        public BranchProtectionPushRestrictions Restrictions { get; protected set; }

        /// <summary>
        /// Specifies whether the protections applied to this branch also apply to repository admins
        /// </summary>
        public EnforceAdmins EnforceAdmins { get; protected set; }

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
        public BranchProtectionRequiredReviewsDismissalRestrictions DismissalRestrictions { get; protected set; }

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
}
