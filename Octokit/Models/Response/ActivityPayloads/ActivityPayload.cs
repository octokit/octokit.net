using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ActivityPayload
    {
        public ActivityPayload() { }


        public ActivityPayload(Repository repository, User sender, InstallationId installation)
        {
            Repository = repository;
            Sender = sender;
            Installation = installation;
        }

        public Repository Repository { get; private set; }
        public User Sender { get; private set; }
        public InstallationId Installation { get; private set; }

        internal string DebuggerDisplay
        {
            get { return Repository.FullName; }
        }
    }
}
