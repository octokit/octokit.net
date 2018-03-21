using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InstallationId
    {
        public InstallationId() { }

        public InstallationId(long id)
        {
            Id = id;
        }

        /// <summary>
        /// The Installation Id.
        /// </summary>
        public long Id { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0}", Id); }
        }
    }
}