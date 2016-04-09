using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateMaintenanceRequest
    {
        public UpdateMaintenanceRequest()
        { }

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


        // Static methods to create MaintenanceDate objects
        public static MaintenanceDate Now()
        {
            return new MaintenanceDate("now");
        }

        public static MaintenanceDate FromDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            return new MaintenanceDate(dateTimeOffset);
        }

        public static MaintenanceDate FromChronicValue(string chronicValue)
        {
            return new MaintenanceDate(chronicValue);
        }
    }
}
