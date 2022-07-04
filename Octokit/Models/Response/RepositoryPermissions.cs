using System.Diagnostics;
using System;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryPermissions
    {
        public RepositoryPermissions() { }

        public RepositoryPermissions(bool admin, bool maintain, bool push, bool triage, bool pull)
        {
            Admin = admin;
            Maintain = maintain;
            Push = push;
            Triage = triage;
            Pull = pull;
        }

        /// <summary>
        /// Whether the current user has administrative permissions
        /// </summary>
        public bool Admin { get; private set;}

        /// <summary>
        /// Whether the current user has maintain permissions
        /// </summary>
        public bool Maintain { get; private set;}

        /// <summary>
        /// Whether the current user has push permissions
        /// </summary>
        public bool Push { get; private set;}

        /// <summary>
        /// Whether the current user has triage permissions
        /// </summary>
        public bool Triage { get; private set;}

        /// <summary>
        /// Whether the current user has pull permissions
        /// </summary>
        public bool Pull { get; private set;}

        internal string DebuggerDisplay
        {
            get
            {
                return FormattableString.Invariant($"Admin: {Admin}, Maintain: {Maintain}, Push: {Push}, Triage: {Triage}, Pull: {Pull}");
            }
        }
    }
}
