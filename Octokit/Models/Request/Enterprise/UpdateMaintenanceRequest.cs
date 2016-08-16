using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateMaintenanceRequest : FormUrlEncodedParameters
    {
        /// <summary>
        /// Maintenance request with default details (results in Maintenance mode being turned off immediately)
        /// </summary>
        public UpdateMaintenanceRequest()
        {
            Maintenance = new UpdateMaintenanceRequestDetails();
        }

        /// <summary>
        /// Maintenance request with specific details
        /// </summary>
        public UpdateMaintenanceRequest(UpdateMaintenanceRequestDetails maintenance)
        {
            Ensure.ArgumentNotNull(maintenance, "maintenance");

            Maintenance = maintenance;
        }

        /// <summary>
        /// Details for the maintenance request
        /// </summary>
        public UpdateMaintenanceRequestDetails Maintenance { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Maintenance: {0}", this.Maintenance.DebuggerDisplay);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateMaintenanceRequestDetails
    {
        /// <summary>
        /// Maintenance request details with default values (results in Maintenance mode being turned off immediately)
        /// </summary>
        public UpdateMaintenanceRequestDetails()
        { }

        /// <summary>
        /// Maintenance request details to enable/disable maintenance mode immediately
        /// </summary>
        /// <param name="enabled">true to enable, false to disable</param>
        public UpdateMaintenanceRequestDetails(bool enabled)
        {
            Enabled = enabled;
            When = "now";
        }

        /// <summary>
        /// Maintenance request details to enable/disable maintenance at a specified time (only applicable when enabling maintenance)
        /// </summary>
        /// <param name="enabled">true to enable, false to disable</param>
        /// <param name="when">A phrase specifying when maintenance mode will be enabled.  Phrase uses humanised forms supported by the <a href="https://github.com/mojombo/chronic">mojombo/chronic library</a> used by the GitHub API</param>
        /// such as "this friday at 5pm" or "5 minutes before midday tomorrow"
        /// <remarks>If enabled is false, the when parameter is ignored and maintenance is turned off immediately</remarks>
        public UpdateMaintenanceRequestDetails(bool enabled, string when)
        {
            Ensure.ArgumentNotNullOrEmptyString(when, "when");

            Enabled = enabled;
            When = when;
        }

        /// <summary>
        /// Maintenance request details to enable/disable maintenance at a specified time (only applicable when enabling maintenance)
        /// </summary>
        /// <param name="enabled">true to enable, false to disable</param>
        /// <param name="when">A <see cref="DateTimeOffset"/> specifying when maintenance mode will be enabled.</param>
        /// <remarks>If enabled is false, the when parameter is ignored and maintenance is turned off immediately</remarks>
        public UpdateMaintenanceRequestDetails(bool enabled, DateTimeOffset when)
        {
            Enabled = enabled;
            When = when.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ",
                          CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Whether maintenance mode is enabled or disabled
        /// </summary>
        public bool Enabled { get; protected set; }

        /// <summary>
        /// When maintenance mode will take effect (only applicable when enabling maintenance)
        /// </summary>
        public string When { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Enabled: {0} When: {1}", this.Enabled, this.When);
            }
        }
    }
}
