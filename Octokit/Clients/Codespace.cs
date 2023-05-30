using System;
using System.Runtime.CompilerServices;

namespace Octokit.Clients
{
    public class Codespace
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public User Owner { get; private set; }
        public User BillableOwner { get; private set; }
        public Repository Repository { get; private set; }
        public Machine Machine { get; private set; }
        public DateTime CreatedAt { get;private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime LastUsedAt { get; private set; }
        public CodespaceState State { get; private set; }
        public string Url { get; private set; }
        public string MachinesUrl { get; private set; }
        public string WebUrl { get; private set; }
        public string StartUrl { get; private set; }
        public string StopUrl { get; private set; }
    }
}