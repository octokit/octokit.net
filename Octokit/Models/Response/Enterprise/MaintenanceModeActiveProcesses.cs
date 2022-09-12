using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MaintenanceModeActiveProcesses
    {
        public MaintenanceModeActiveProcesses() { }

        public MaintenanceModeActiveProcesses(string name, int number)
        {
            Name = name;
            Number = number;
        }

        public string Name { get; private set; }

        public int Number { get; private set; }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", Name, Number);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return this.ToString();
            }
        }
    }
}
