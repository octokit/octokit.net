namespace Octokit
{
    public class Machine
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string OperatingSystem { get; private set; }
        public long StorageInBytes { get; private set; }
        public long MemoryInBytes { get; private set; }
        public long CpuCount { get; private set; }
    }
}