using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationMembership
    {
        public OrganizationMembership()
        {
            
        }

        public OrganizationMembership(string url, StringEnum<MembershipState> state, StringEnum<MembershipRole> role, string organizationUrl, Organization organization, User user)
        {
            Url = url;
            State = state;
            Role = role;
            OrganizationUrl = organizationUrl;
            Organization = organization;
            User = user;
        }
        
        public string Url { get; protected set; }
        public StringEnum<MembershipState> State { get; protected set; }
        public StringEnum<MembershipRole> Role { get; protected set; }
        public string OrganizationUrl { get; protected set; }
        public Organization Organization { get; protected set; }
        public User User { get; protected set; }

        internal string DebuggerDisplay => $"{nameof(OrganizationMembership)}: User: {User.Login}; Organization: {Organization.Login}; State: {State}; Role: {Role}";
    }
}
