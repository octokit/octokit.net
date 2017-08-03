using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class OrganizationOutsideCollaboratorsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationOutsideCollaboratorsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                client.GetAll("org");

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators"), null, "application/vnd.github.korra-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationOutsideCollaboratorsClient(Substitute.For<IApiConnection>());


                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All));
            }

            [Fact]
            public void AllFilterRequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                client.GetAll("org", OrganizationMembersFilter.All);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=all"), null, "application/vnd.github.korra-preview+json");
            }

            [Fact]
            public void TwoFactorFilterRequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                client.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=2fa_disabled"), null, "application/vnd.github.korra-preview+json");
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                client.Delete("org", "user");

                connection.Connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators/user"), 
                    Arg.Any<object>(), 
                    "application/vnd.github.korra-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationOutsideCollaboratorsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "user"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("org", null));
                
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "user"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("org", ""));
            }
        }

        public class TheConvertFromMemberMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                client.ConvertFromMember("org", "user");

                connection.Connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators/user"),
                    "application/vnd.github.korra-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArgument()
            {
                var client = new OrganizationOutsideCollaboratorsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ConvertFromMember(null, "user"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ConvertFromMember("org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.ConvertFromMember("", "user"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ConvertFromMember("org", ""));
            }
        }
    }
}
