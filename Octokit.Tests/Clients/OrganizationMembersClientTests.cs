using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

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
            public void EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => orgMembers.GetAll(null));
                Assert.Throws<ArgumentException>(() => orgMembers.GetAll(""));
            }
        }

        public class TheGetPublicMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgMembers = new OrganizationMembersClient(client);

                orgMembers.GetPublic("org");

                client.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members"));
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => orgMembers.GetPublic(null));
                Assert.Throws<ArgumentException>(() => orgMembers.GetPublic(""));
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
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
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
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = HttpStatusCode.Conflict });
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/members/username"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                await AssertEx.Throws<ApiException>(async () => await client.CheckMember("org", "username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.CheckMember(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.CheckMember(null, ""));
                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.CheckMember("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.CheckMember("", null));
            }
        }

        public class TheCheckMemberPublicMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
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
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = HttpStatusCode.Conflict });
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members/username"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                await AssertEx.Throws<ApiException>(async () => await client.CheckMemberPublic("org", "username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.CheckMemberPublic(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.CheckMemberPublic("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.CheckMemberPublic("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.CheckMemberPublic("org", ""));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.Delete(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.Delete("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.Delete("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.Delete("org", ""));
            }
        }

        public class ThePublicizeMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
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
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = HttpStatusCode.Conflict });
                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/public_members/username"),
                    new { }).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new OrganizationMembersClient(apiConnection);

                await AssertEx.Throws<ApiException>(async () => await client.Publicize("org", "username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.Publicize(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.Publicize("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.Publicize("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.Publicize("org", ""));
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

                connection.Received().Delete(Arg.Is<Uri>(u=>u.ToString() == "orgs/org/public_members/username"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var orgMembers = new OrganizationMembersClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.Conceal(null, "username"));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.Conceal("", "username"));
                await AssertEx.Throws<ArgumentNullException>(async () => await orgMembers.Conceal("org", null));
                await AssertEx.Throws<ArgumentException>(async () => await orgMembers.Conceal("org", ""));
            }
        }
    }
}
