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

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators"), null, Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll("org", options);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators"), null, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationOutsideCollaboratorsClient(Substitute.For<IApiConnection>());


                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("org", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("org", OrganizationMembersFilter.All, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All, ApiOptions.None));
            }

            [Fact]
            public void AllFilterRequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                client.GetAll("org", OrganizationMembersFilter.All);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=all"), null, Args.ApiOptions);
            }

            [Fact]
            public void AllFilterRequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll("org", OrganizationMembersFilter.All, options);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=all"), null, options);
            }

            [Fact]
            public void TwoFactorFilterRequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                client.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=2fa_disabled"), null, Args.ApiOptions);
            }

            [Fact]
            public void TwoFactorFilterRequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationOutsideCollaboratorsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, options);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators?filter=2fa_disabled"), null, options);
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

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators/user"));
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

                connection.Connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "orgs/org/outside_collaborators/user"));
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
