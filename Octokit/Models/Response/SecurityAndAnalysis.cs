using Octokit.Internal;
using System.Diagnostics;


namespace Octokit
{
    /// <summary>
    /// The security and analysis features that are enabled or disabled for the repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SecurityAndAnalysis
    {
        public SecurityAndAnalysis()
        { }

        public SecurityAndAnalysis(AdvancedSecurity advancedSecurity, DependabotSecurityUpdates dependabotSecurityUpdates, SecretScanning secretScanning, SecretScanningPushProtection secretScanningPushProtection, SecretScanningValidityChecks secretScanningValidityChecks)
        {
            this.AdvancedSecurity = advancedSecurity;
            this.DependabotSecurityUpdates = dependabotSecurityUpdates;
            this.SecretScanning = secretScanning;
            this.SecretScanningPushProtection = secretScanningPushProtection;
            this.SecretScanningValidityChecks = secretScanningValidityChecks;
        }


        public AdvancedSecurity AdvancedSecurity { get; protected set; }

        public DependabotSecurityUpdates DependabotSecurityUpdates { get; protected set; }

        public SecretScanning SecretScanning { get; protected set; }

        public SecretScanningPushProtection SecretScanningPushProtection { get; protected set; }

        public SecretScanningValidityChecks SecretScanningValidityChecks { get; protected set; }


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
    public class AdvancedSecurity
    {
        public AdvancedSecurity()
        { }

        public AdvancedSecurity(string status)
        {
            this.Status = status;
        }

        
        public string Status { get; protected set; }

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
    public class DependabotSecurityUpdates
    {
        public DependabotSecurityUpdates()
        { }

        public DependabotSecurityUpdates(string status)
        {
            this.Status = status;
        }


        public string Status { get; protected set; }

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
    public class SecretScanning
    {
        public SecretScanning()
        { }

        public SecretScanning(string status)
        {
            this.Status = status;
        }


        public string Status { get; protected set; }

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
    public class SecretScanningPushProtection
    {
        public SecretScanningPushProtection()
        { }

        public SecretScanningPushProtection(string status)
        {
            this.Status = status;
        }


        public string Status { get; protected set; }

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
    public class SecretScanningValidityChecks
    {
        public SecretScanningValidityChecks()
        { }

        public SecretScanningValidityChecks(string status)
        {
            this.Status = status;
        }


        public string Status { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return new SimpleJsonSerializer().Serialize(this);
            }
        }
    }
   
}
