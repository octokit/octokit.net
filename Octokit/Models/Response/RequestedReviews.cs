using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Users and teams requested to review a pull request
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RequestedReviews
    {
        public RequestedReviews()
        {
            Users = new List<User>();
            Teams = new List<Team>();
        }

        public RequestedReviews(IReadOnlyList<User> users, IReadOnlyList<Team> teams)
        {
            Users = users;
            Teams = teams;
        }
        public IReadOnlyList<User> Users { get; private set; }
        public IReadOnlyList<Team> Teams { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
              CultureInfo.InvariantCulture,
              "Users: {0}, Teams: {1}",
              string.Join(", ", Users.Select(u => u.Login)),
              string.Join(", ", Teams.Select(t => t.Slug)));
            }
        }
    }
}
