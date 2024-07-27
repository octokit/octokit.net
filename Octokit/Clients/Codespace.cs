using Octokit.Clients;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Codespace
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public User Owner { get; private set; }
        public User BillableOwner { get; private set; }
        public Repository Repository { get; private set; }
        public Machine Machine { get; private set; }
        public DateTime CreatedAt { get;private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime LastUsedAt { get; private set; }
        public StringEnum<CodespaceState> State { get; private set; }
        public string Url { get; private set; }
        public string MachinesUrl { get; private set; }
        public string WebUrl { get; private set; }
        public string StartUrl { get; private set; }
        public string StopUrl { get; private set; }
        public StringEnum<CodespaceLocation> Location { get; private set; }

        public Codespace(long id, string name, User owner, User billableOwner, Repository repository, Machine machine, DateTime createdAt, DateTime updatedAt, DateTime lastUsedAt, StringEnum<CodespaceState> state, string url, string machinesUrl, string webUrl, string startUrl, string stopUrl, StringEnum<CodespaceLocation> location)
        {
            Id = id;
            Name = name;
            Owner = owner;
            BillableOwner = billableOwner;
            Repository = repository;
            Machine = machine;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            LastUsedAt = lastUsedAt;
            State = state;
            Url = url;
            MachinesUrl = machinesUrl;
            WebUrl = webUrl;
            StartUrl = startUrl;
            StopUrl = stopUrl;
            Location = location;
        }

        public Codespace() { }
        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "Codespace: Id: {0}", Id);
    }
}
