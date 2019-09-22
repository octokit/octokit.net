using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MaintenanceModeResponse
    {
        public MaintenanceModeResponse() { }

        public MaintenanceModeResponse(MaintenanceModeStatus status, string scheduledTime, IReadOnlyList<MaintenanceModeActiveProcesses> activeProcesses)
        {
            Status = status;
            ScheduledTime = scheduledTime;
            ActiveProcesses = activeProcesses;
        }

        public StringEnum<MaintenanceModeStatus> Status
        {
            get;
            private set;
        }

        public string ScheduledTime
        {
            get;
            private set;
        }

        [Parameter(Key = "connection_services")]
        public IReadOnlyList<MaintenanceModeActiveProcesses> ActiveProcesses
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Status: {0} ScheduledTime: {1} Connections: {2}",
                    Status,
                    ScheduledTime ?? "",
                    string.Join(", ", ActiveProcesses));
            }
        }
    }

    public enum MaintenanceModeStatus
    {
        [Parameter(Value = "off")]
        Off,
        [Parameter(Value = "on")]
        On,
        [Parameter(Value = "scheduled")]
        Scheduled
    }
}