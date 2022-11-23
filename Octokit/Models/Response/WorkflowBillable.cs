﻿using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowBillable
    {
        public WorkflowBillable() { }

        public WorkflowBillable(WorkflowBillableTiming ubuntu, WorkflowBillableTiming macOS, WorkflowBillableTiming windows)
        {
            Ubuntu = ubuntu;
            MacOS = macOS;
            Windows = windows;
        }

        /// <summary>
        /// The Ubuntu billable timing.
        /// </summary>
        [Parameter(Key = "UBUNTU")]
        public WorkflowBillableTiming Ubuntu { get; private set; }

        /// <summary>
        /// The macOS billable timing.
        /// </summary>
        [Parameter(Key = "MACOS")]
        public WorkflowBillableTiming MacOS { get; private set; }

        /// <summary>
        /// The Windows billable timing.
        /// </summary>
        [Parameter(Key = "WINDOWS")]
        public WorkflowBillableTiming Windows { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Ubuntu: {0}, macOS: {1}, Windows: {2}", Ubuntu, MacOS, Windows);
            }
        }
    }
}
