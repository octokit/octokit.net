using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SourceInfo
    {
        public SourceInfo() { }

        public SourceInfo(User actor, int id, Issue issue, string url)
        {
            Actor = actor;
            Id = id;
            Issue = issue;
            Url = url;
        }

        public User Actor { get; private set; }
        public int Id { get; private set; }
        public Issue Issue { get; private set; }
        public string Url { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Url: {1}", Id, Url ?? ""); }
        }
    }
}
