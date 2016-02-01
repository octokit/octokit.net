using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdminStats
    {
        public AdminStats() { }

        public AdminStats(AdminStatsRepos repos, AdminStatsHooks hooks, AdminStatsPages pages, AdminStatsOrgs orgs, AdminStatsUsers users, AdminStatsPulls pulls, AdminStatsIssues issues, AdminStatsMilestones milestones, AdminStatsGists gists, AdminStatsComments comments)
        {
            Repos = repos;
            Hooks = hooks;
            Pages = pages;
            Orgs = orgs;
            Users = users;
            Pulls = pulls;
            Issues = issues;
            Milestones = milestones;
            Gists = gists;
            Comments = comments;
        }

        public AdminStatsRepos Repos
        {
            get;
            private set;
        }

        public AdminStatsHooks Hooks
        {
            get;
            private set;
        }

        public AdminStatsPages Pages
        {
            get;
            private set;
        }

        public AdminStatsOrgs Orgs
        {
            get;
            private set;
        }

        public AdminStatsUsers Users
        {
            get;
            private set;
        }

        public AdminStatsPulls Pulls
        {
            get;
            private set;
        }

        public AdminStatsIssues Issues
        {
            get;
            private set;
        }

        public AdminStatsMilestones Milestones
        {
            get;
            private set;
        }

        public AdminStatsGists Gists
        {
            get;
            private set;
        }

        public AdminStatsComments Comments
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                string fieldsPresent = String.Concat(
                    Repos != null ? "Repos," : "",
                    Hooks != null ? "Hooks," : "",
                    Pages != null ? "Pages," : "",
                    Orgs != null ? "Orgs," : "",
                    Users != null ? "Users," : "",
                    Pulls != null ? "Pulls," : "",
                    Issues != null ? "Issues," : "",
                    Milestones != null ? "Milestones," : "",
                    Gists != null ? "Gists," : "",
                    Comments != null ? "Comments," : ""
                    ).Trim(',');

                return String.Format(CultureInfo.InvariantCulture, "Statistics: {0}", fieldsPresent);
            }
        }
    }
}