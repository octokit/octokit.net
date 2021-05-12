using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class HeadCommitUser
    {
        public HeadCommitUser()
        {
        }

        public HeadCommitUser(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; protected set; }
        public string Email { get; protected set; }

        internal string DebuggerDisplay
            => $"Name: {Name}, Email: {Email}";
    }
}
