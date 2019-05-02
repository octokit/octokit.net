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

        public User Actor { get; protected set; }
        public int Id { get; protected set; }
        public Issue Issue { get; protected set; }
        public string Url { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Url: {1}", Id, Url ?? ""); }
        }
    }
}
