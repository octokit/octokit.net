using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Machine
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string OperatingSystem { get; private set; }
        public long StorageInBytes { get; private set; }
        public long MemoryInBytes { get; private set; }
        public long CpuCount { get; private set; }

        public Machine(string name, string displayName, string operatingSystem, long storageInBytes, long memoryInBytes, long cpuCount)
        {
            Name = name;
            DisplayName = displayName;
            OperatingSystem = operatingSystem;
            StorageInBytes = storageInBytes;
            MemoryInBytes = memoryInBytes;
            CpuCount = cpuCount;
        }

        public Machine() { }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "Machine: {0}", DisplayName);
    }
}