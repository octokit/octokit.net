using Octokit.Internal;

using System;
using System.Diagnostics;
using System.Globalization;


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
			AdvancedSecurity = advancedSecurity;
			DependabotSecurityUpdates = dependabotSecurityUpdates;
			SecretScanning = secretScanning;
			SecretScanningPushProtection = secretScanningPushProtection;
			SecretScanningValidityChecks = secretScanningValidityChecks;
		}


		public AdvancedSecurity AdvancedSecurity { get; private set; }
		
		public DependabotSecurityUpdates DependabotSecurityUpdates { get; private set; }
		
		public SecretScanning SecretScanning { get; private set; }
		
		public SecretScanningPushProtection SecretScanningPushProtection { get; private set; }
		
		public SecretScanningValidityChecks SecretScanningValidityChecks { get; private set; }


		internal string DebuggerDisplay
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture, "{0}", this);
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

		public AdvancedSecurity(Status status)
		{
			Status = status;
		}


		public StringEnum<Status> Status { get; private set; }

		internal string DebuggerDisplay
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture, "{0}", this);
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

		public DependabotSecurityUpdates(Status status)
		{
			Status = status;
		}


		public StringEnum<Status> Status { get; private set; }

		internal string DebuggerDisplay
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture, "{0}", this);
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

		public SecretScanning(Status status)
		{
			Status = status;
		}


		public StringEnum<Status> Status { get; private set; }

		internal string DebuggerDisplay
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture, "{0}", this);
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

		public SecretScanningPushProtection(Status status)
		{
			Status = status;
		}	


		public StringEnum<Status> Status { get; private set; }

		internal string DebuggerDisplay
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture, "{0}", this);
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

		public SecretScanningValidityChecks(Status status)
		{
			Status Status = status;
		}


		public StringEnum<Status> Status { get; private set; }

		internal string DebuggerDisplay
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture, "{0}", this);
			}
		}
	}


	public enum Status
	{
		[Parameter(Value = "Enabled")]
		Enabled,
		[Parameter(Value = "Disabled")]
		Disabled
	}
}
