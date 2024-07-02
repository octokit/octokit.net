using Octokit.Internal;
using System.Diagnostics;


namespace Octokit
{
    /// <summary>
    /// The security and analysis features that are enabled or disabled for the repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SecurityAndAnalysisRequest
    {
        public SecurityAndAnalysisRequest()
        { }

        public SecurityAndAnalysisRequest(AdvancedSecurityRequest advancedSecurity, DependabotSecurityUpdatesRequest dependabotSecurityUpdates, 
            SecretScanningRequest secretScanning, SecretScanningPushProtectionRequest secretScanningPushProtection, 
            SecretScanningValidityChecksRequest secretScanningValidityChecks)
        {
            this.AdvancedSecurity = advancedSecurity;
            this.DependabotSecurityUpdates = dependabotSecurityUpdates;
            this.SecretScanning = secretScanning;
            this.SecretScanningPushProtection = secretScanningPushProtection;
            this.SecretScanningValidityChecks = secretScanningValidityChecks;
        }


        public AdvancedSecurityRequest AdvancedSecurity { get; set; }
        public DependabotSecurityUpdatesRequest DependabotSecurityUpdates { get; set; }
        public SecretScanningRequest SecretScanning { get; set; }
        public SecretScanningPushProtectionRequest SecretScanningPushProtection { get; set; }
        public SecretScanningValidityChecksRequest SecretScanningValidityChecks { get; set; }


        internal string DebuggerDisplay
        {
            get
            {
                return new SimpleJsonSerializer().Serialize(this);
            }
        }
    }


    /// <summary>
    /// Use the status property to enable or disable GitHub Advanced Security for this repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdvancedSecurityRequest
    {
        public AdvancedSecurityRequest()
        { }

        public AdvancedSecurityRequest(Status? status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Can be enabled, disabled, or null
        /// </summary>
        public Status? Status { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return new SimpleJsonSerializer().Serialize(this);
            }
        }
    }

    /// <summary>
    /// Use the status property to enable or disable Dependabot security updates for this repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependabotSecurityUpdatesRequest
    {
        public DependabotSecurityUpdatesRequest()
        { }

        public DependabotSecurityUpdatesRequest(Status? status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Can be enabled, disabled, or null
        /// </summary>
        public Status? Status { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return new SimpleJsonSerializer().Serialize(this);
            }
        }
    }

    /// <summary>
    /// Use the status property to enable or disable secret scanning for this repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SecretScanningRequest
    {
        public SecretScanningRequest()
        { }

        public SecretScanningRequest(Status? status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Can be enabled, disabled, or null
        /// </summary>
        public Status? Status { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return new SimpleJsonSerializer().Serialize(this);
            }
        }
    }

    /// <summary>
    /// Use the status property to enable or disable secret scanning push protection for this repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SecretScanningPushProtectionRequest
    {
        public SecretScanningPushProtectionRequest()
        { }

        public SecretScanningPushProtectionRequest(Status? status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Can be enabled, disabled, or null
        /// </summary>
        public Status? Status { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return new SimpleJsonSerializer().Serialize(this);
            }
        }
    }

    /// <summary>
    /// Use the status property to enable or disable secret scanning validity checks for this repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SecretScanningValidityChecksRequest
    {
        public SecretScanningValidityChecksRequest()
        { }

        public SecretScanningValidityChecksRequest(Status? status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Can be enabled, disabled, or null
        /// </summary>
        public Status? Status { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return new SimpleJsonSerializer().Serialize(this);
            }
        }
    }
}
