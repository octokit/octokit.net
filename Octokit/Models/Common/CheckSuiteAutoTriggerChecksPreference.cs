using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteAutoTriggerChecksPreference
    {
        public CheckSuiteAutoTriggerChecksPreference()
        {
        }

        public CheckSuiteAutoTriggerChecksPreference(long appId, bool setting)
        {
            AppId = appId;
            Setting = setting;
        }

        public long AppId { get; protected set; }
        public bool Setting { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "{0}: {1}", AppId, Setting ? "On" : "Off");
    }
}