﻿using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CombinedCommitStatus
    {
        public CombinedCommitStatus() { }

        public CombinedCommitStatus(string sha, int totalCount, IReadOnlyList<CommitStatus> statuses, Repository repository)
        {
            Sha = sha;
            TotalCount = totalCount;
            Statuses = statuses;
            Repository = repository;
        }

        /// <summary>
        /// The combined state of the commits.
        /// </summary>
        [Parameter(Key = "IgnoreThisField")]
        public CommitState? State { get { return StateText.ParseEnumWithDefault(CommitState.Unknown); } }

        [Parameter(Key = "state")]
        public string StateText { get; protected set; }

        /// <summary>
        /// The SHA of the reference.
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// The total number of statuses.
        /// </summary>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// The statuses.
        /// </summary>
        public IReadOnlyList<CommitStatus> Statuses { get; protected set; }

        /// <summary>
        /// The repository of the reference.
        /// </summary>
        public Repository Repository { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "SHA: {0}, State: {1}, TotalCount: {2}", Sha, State, TotalCount);
            }
        }
    }
}