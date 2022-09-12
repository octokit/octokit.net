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

        public string Url { get; private set; }
        public StringEnum<MembershipState> State { get; private set; }
        public StringEnum<MembershipRole> Role { get; private set; }
        public string OrganizationUrl { get; private set; }
        public Organization Organization { get; private set; }
        public User User { get; private set; }

        internal string DebuggerDisplay => $"{nameof(OrganizationMembership)}: User: {User.Login}; Organization: {Organization.Login}; State: {State}; Role: {Role}";
    }
}
