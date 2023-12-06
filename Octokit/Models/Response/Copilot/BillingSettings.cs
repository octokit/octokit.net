using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// The billing settings for a Copilot-enabled organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class BillingSettings
    {
        /// <summary>
        /// A summary of the current billing settings for the organization.
        /// </summary>
        public SeatBreakdown SeatBreakdown { get; set; }
    
        /// <summary>
        /// A string that indicates how seats are billed for the organization. 
        /// </summary>
        public string SeatManagementSetting { get; set; }
    
        /// <summary>
        /// A string that indicates if public code suggestions are enabled or blocked for the organization.
        /// </summary>
        public string PublicCodeSuggestions { get; set; }
        
        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "SeatManagementSetting: {0}, PublicCodeSuggestions: {1}", SeatManagementSetting, PublicCodeSuggestions); }
        }
    }
}

