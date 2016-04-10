using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateMaintenanceRequest
    {
        /// <summary>
        /// Maintenance request with default values (results in Maintenance mode being turned off immediately)
        /// </summary>
        public UpdateMaintenanceRequest()
        { }

        /// <summary>
        /// Maintenance request specifying whether to enable/disable maintenance and when (only applicable when enabling maintenance)
        /// </summary>
        /// <param name="enabled">true to enable, false to disable</param>
        /// <param name="when">A <see cref="MaintenanceDate"/> object specifying when maintenance mode will be enabled.
        /// Use static helper methods
        ///   <see cref="MaintenanceDate.Now()"/>
        ///   <see cref="MaintenanceDate.FromDateTimeOffset(DateTimeOffset)"/> and
        ///   <see cref="MaintenanceDate.FromChronicValue(string)"/></param>
        /// <remarks>If enabled is false, the when parameter is ignored and maintenance is turned off immediately</remarks>
        public UpdateMaintenanceRequest(bool enabled, MaintenanceDate when)
        {
            Enabled = enabled;
            When = when;
        }

        public bool Enabled { get; protected set; }
        
        public MaintenanceDate When { get; protected set; }

        [SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower")]
        public string AsJsonString()
        {
            var json = string.Format(CultureInfo.InvariantCulture,
                "\"enabled\":{0}",
                Enabled.ToString().ToLower());

            if (Enabled)
            {
                json = string.Concat(json, string.Format(CultureInfo.InvariantCulture,
                ", \"when\":\"{0}\"",
                When.Value));
            }

            json = string.Concat("{", json, "}");

            return json;
        }

        public string AsNamedFormEncodingString()
        {
            var value = string.Format(CultureInfo.InvariantCulture, "maintenance={0}", this.AsJsonString());
            return value;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Maintenance: {0}", this.AsJsonString());
            }
        }
    }

    /// <summary>
    /// Represents when maintenance will be enabled.
    /// Created by static helper methods
    ///   <see cref="MaintenanceDate.Now()"/>
    ///   <see cref="MaintenanceDate.FromDateTimeOffset(DateTimeOffset)"/> and
    ///   <see cref="MaintenanceDate.FromChronicValue(string)"/>
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MaintenanceDate
    {
        private string _chronicString;
        private DateTimeOffset _dateTimeObject;

        private MaintenanceDate(string chronicDateString)
        {
            _chronicString = chronicDateString;
        }

        private MaintenanceDate(DateTimeOffset dateTimeOffset)
        {
            _dateTimeObject = dateTimeOffset;
        }

        /// <summary>
        /// Provides the <see cref="MaintenanceDate"/> value in ISO8601 date format
        /// </summary>
        public string Value
        {
            get
            {
                if (!string.IsNullOrEmpty(_chronicString))
                {
                    return _chronicString;
                }

                if (_dateTimeObject != null)
                {
                    return _dateTimeObject.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ",
                          CultureInfo.InvariantCulture);
                }

                return "";
            }
        }

        internal string DebuggerDisplay
        {
            get
            {
                return Value;
            }
        }


        /// <summary>
        /// Static helper to create a <see cref="MaintenanceDate"/> that is immediate
        /// </summary>
        public static MaintenanceDate Now()
        {
            return new MaintenanceDate("now");
        }

        /// <summary>
        /// Static helper to create a <see cref="MaintenanceDate"/> from a <see cref="DateTimeOffset"/> value
        /// </summary>
        public static MaintenanceDate FromDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            return new MaintenanceDate(dateTimeOffset);
        }

        /// <summary>
        /// Static helper to create a <see cref="MaintenanceDate"/> using the humanised forms supported by the <a href="https://github.com/mojombo/chronic">mojombo/chronic library</a> used by the GitHub API
        /// such as "this friday at 5pm" or "5 minutes before midday tomorrow"
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/management_console/#enable-or-disable-maintenance-mode">Enable or disable maintenance mode</a> API documentation for more information.
        /// </remarks>
        public static MaintenanceDate FromChronicValue(string chronicValue)
        {
            return new MaintenanceDate(chronicValue);
        }
    }
}
