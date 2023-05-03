using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerLabel
    {
        public RunnerLabel() { }

        public RunnerLabel(long id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0}; Name: {1}; Type: {2};", Id, Name, Type); }
        }
    }
}
