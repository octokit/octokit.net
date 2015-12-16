using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    // Corresponds to org_schema
    public class OrganizationSimple : AccountSimple
    {
        public string ReposUrl { get; protected set; }
        public string EventsUrl { get; protected set; }
        public string PublicMembersUrl { get; protected set; }
        public string Description { get; protected set; }
        public string Blog { get; protected set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PublicRepos { get; set; }
        public int PublicGists { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
    }
}
