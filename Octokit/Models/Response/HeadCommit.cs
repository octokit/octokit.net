using System;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class HeadCommit
    {
        public HeadCommit()
        {
        }

        public HeadCommit(
            string id, string treeId, string message, DateTimeOffset timestamp,
            HeadCommitUser author, HeadCommitUser committer)
        {
            Id = id;
            TreeId = treeId;
            Message = message;
            Timestamp = timestamp;
            Author = author;
            Committer = committer;
        }

        public string Id { get; protected set; }

        public string TreeId { get; protected set; }

        public string Message { get; protected set; }

        public DateTimeOffset Timestamp { get; protected set; }

        public HeadCommitUser Author { get; protected set; }

        public HeadCommitUser Committer { get; protected set; }

        internal string DebuggerDisplay
            => $"Id: {Id}, Message: {Message}, Timestamp: {Timestamp}";
    }
}
