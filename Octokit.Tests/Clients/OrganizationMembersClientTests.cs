using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class OrganizationMembersClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
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

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                orgMembersClient.GetAll("org", options);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members"), options);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("org", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All, ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("org", OrganizationMembersFilter.All, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All, OrganizationMembersRole.Admin));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, OrganizationMembersFilter.All, OrganizationMembersRole.Admin, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("org", OrganizationMembersFilter.All, OrganizationMembersRole.Admin, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All, OrganizationMembersRole.Admin));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", OrganizationMembersFilter.All, OrganizationMembersRole.Admin, ApiOptions.None));
            }

            [Fact]
            public void AllFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.All);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=all"), Args.ApiOptions);
            }

            [Fact]
            public void TwoFactorFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled"), Args.ApiOptions);
            }

            [Fact]
            public void TwoFactorFilterRequestTheCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, options);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled"), options);
            }

            [Fact]
            public void AllRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersRole.All);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?role=all"), Args.ApiOptions);
            }

            [Fact]
            public void AdminRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersRole.Admin);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?role=admin"), Args.ApiOptions);
            }

            [Fact]
            public void MemberRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersRole.Member);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?role=member"), Args.ApiOptions);
            }

            [Fact]
            public void MemberRoleFilterRequestTheCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                orgMembersClient.GetAll("org", OrganizationMembersRole.Member, options);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?role=member"), options);
            }

            [Fact]
            public void AllFilterPlusAllRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.All, OrganizationMembersRole.All);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=all&role=all"), Args.ApiOptions);
            }

            [Fact]
            public void AllFilterPlusAdminRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.All, OrganizationMembersRole.Admin);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=all&role=admin"), Args.ApiOptions);
            }

            [Fact]
            public void AllFilterPlusMemberRoleFilterRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.All, OrganizationMembersRole.Member);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=all&role=member"), Args.ApiOptions);
            }

            [Fact]
            public void TwoFactorFilterPlusAllRoleRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.All);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled&role=all"), Args.ApiOptions);
            }

            [Fact]
            public void TwoFactorFilterPlusAdminRoleRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.Admin);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled&role=admin"), Args.ApiOptions);
            }

            [Fact]
            public void TwoFactorFilterPlusMemberRoleRequestTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.Member);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled&role=member"), Args.ApiOptions);
            }

            [Fact]
            public void TwoFactorFilterPlusMemberRoleRequestTheCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembersClient = new OrganizationMembersClient(client);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                orgMembersClient.GetAll("org", OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.Member, options);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members?filter=2fa_disabled&role=member"), options);
            }
        }

        public class TheGetPublicMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembers = new OrganizationMembersClient(client);

                await orgMembers.GetAllPublic("org");

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembers = new OrganizationMembersClient(client);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await orgMembers.GetAllPublic("org", options);

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members"), options);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPublic(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPublic(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPublic("org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllPublic(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllPublic("", ApiOptions.None));
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

        public class TheGetOrganizationMembershipMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationMembersClient(connection);

                client.GetOrganizationMembership("org", "username");

                connection.Received().Get<OrganizationMembership>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/memberships/username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetOrganizationMembership(null, "username"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetOrganizationMembership("", "username"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetOrganizationMembership("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetOrganizationMembership("org", ""));
            }
        }

        public class TheAddOrUpdateOrganizationMembershipMethod
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var orgMembershipUpdate = new OrganizationMembershipUpdate();
                  
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationMembersClient(connection);
                
                client.AddOrUpdateOrganizationMembership("org", "username", orgMembershipUpdate);

                connection.Received().Put<OrganizationMembership>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/memberships/username"), Arg.Any<object>());
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new OrganizationMembersClient(Substitute.For<IApiConnection>());
                var orgMembershipUpdate = new OrganizationMembershipUpdate();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrUpdateOrganizationMembership(null, "username", orgMembershipUpdate));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddOrUpdateOrganizationMembership("", "username", orgMembershipUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrUpdateOrganizationMembership("org", null, orgMembershipUpdate));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddOrUpdateOrganizationMembership("org", "", orgMembershipUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrUpdateOrganizationMembership("org", "username", null));
            }
        }

        public class TheDeleteOrganizationMembershipMethod
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationMembersClient(connection);

                client.RemoveOrganizationMembership("org", "username");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/memberships/username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveOrganizationMembership(null, "username"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveOrganizationMembership("", "username"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveOrganizationMembership("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveOrganizationMembership("org", ""));
            }
        }

        public class TheGetAllPendingInvitationsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationMembersClient(connection);

                client.GetAllPendingInvitations("org");

                connection.Received().GetAll<OrganizationMembershipInvitation>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/invitations"), null, "application/vnd.github.korra-preview+json", Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationMembersClient(connection);
                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };
                client.GetAllPendingInvitations("org", options);

                connection.Received().GetAll<OrganizationMembershipInvitation>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/invitations"), null, "application/vnd.github.korra-preview+json", options);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPendingInvitations(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllPendingInvitations(""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPendingInvitations(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllPendingInvitations("", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllPendingInvitations("org", null));
            }
        }
    }
}
