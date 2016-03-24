using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class OrganizationMembersClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsureNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationMembersClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org");

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.GetAll(null));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.GetAll(""));
            }

            [Fact]
            public void AllFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.All);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=all"));
            }

            [Fact]
            public void TwoFactorFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled"));
            }

            [Fact]
            public void AllRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersRole.All);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?role=all"));
            }

            [Fact]
            public void AdminRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersRole.Admin);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?role=admin"));
            }

            [Fact]
            public void MemberRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersRole.Member);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?role=member"));
            }

            [Fact]
            public void AllFilterPlusAllRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.All, OrganizationMembersRole.All);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=all&role=all"));
            }

            [Fact]
            public void AllFilterPlusAdminRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.All, OrganizationMembersRole.Admin);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=all&role=admin"));
            }

            [Fact]
            public void AllFilterPlusMemberRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.All, OrganizationMembersRole.Member);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=all&role=member"));
            }

            [Fact]
            public void TwoFactorFilterPlusAllRoleRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.All);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled&role=all"));
            }

            [Fact]
            public void TwoFactorFilterPlusAdminRoleRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.Admin);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled&role=admin"));
            }

            [Fact]
            public void TwoFactorFilterPlusMemberRoleRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.Member);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled&role=member"));
            }
        }

        public class TheGetPublicMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembers = new OrganizationMembersClient(client);

                orgMembers.GetAllPublic("org");

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.GetAllPublic(null));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.GetAllPublic(""));
            }
        }

        public class TheCheckMemberMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            [InlineData(HttpStatusCode.Found, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members/username"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                var result = await client.CheckMember("org", "username");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members/username"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.CheckMember("org", "username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.CheckMember(null, "username"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.CheckMember(null, ""));
                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.CheckMember("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.CheckMember("", null));
            }
        }

        public class TheCheckMemberPublicMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members/username"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                var result = await client.CheckMemberPublic("org", "username");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members/username"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.CheckMemberPublic("org", "username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.CheckMemberPublic(null, "username"));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.CheckMemberPublic("", "username"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.CheckMemberPublic("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.CheckMemberPublic("org", ""));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationMembersClient(connection);

                client.Delete("org", "username");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members/username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.Delete(null, "username"));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.Delete("", "username"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.Delete("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.Delete("org", ""));
            }
        }

        public class ThePublicizeMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members/username"),
                    Args.Object).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                var result = await client.Publicize("org", "username");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members/username"),
                    new { }).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.Publicize("org", "username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.Publicize(null, "username"));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.Publicize("", "username"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.Publicize("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.Publicize("org", ""));
            }
        }

        public class TheConcealMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationMembersClient(connection);

                client.Conceal("org", "username");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members/username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.Conceal(null, "username"));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.Conceal("", "username"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => orgMembers.Conceal("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => orgMembers.Conceal("org", ""));
            }
        }
    }
}
