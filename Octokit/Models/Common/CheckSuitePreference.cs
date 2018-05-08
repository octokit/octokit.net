namespace Octokit
{
    public class CheckSuitePreference
    {
        public CheckSuitePreference()
        {
        }
        public CheckSuitePreference(long appId, bool setting)
        {
            AppId = appId;
            Setting = setting;
        }

        public long AppId { get; protected set; }
        public bool Setting { get; protected set; }
    }
}