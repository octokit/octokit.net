using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class OrganizationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.Get("orgName");

                connection.Received().Get<Organization>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgName"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(""));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.GetAllForUser("username");

                connection.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "users/username/orgs"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForUser("username", options);

                connection.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "users/username/orgs"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("username", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("", ApiOptions.None));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.GetAllForCurrent();

                connection.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "user/orgs"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForCurrent(options);

                connection.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "user/orgs"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(null));
            }
        }

        public class TheGetAllOrganizationsMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.GetAll();

                connection.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "organizations"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRequestParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                var request = new OrganizationRequest(1);

                await client.GetAll(request);

                connection.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "organizations?since=1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll((OrganizationRequest)null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.Update("initrode", new OrganizationUpdate());

                connection.Received().Patch<Organization>(Arg.Is<Uri>(u => u.ToString() == "orgs/initrode"), Args.OrganizationUpdate);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, new OrganizationUpdate()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", new OrganizationUpdate()));
            }
        }

        public class TheGetAllAuthorizationsMethod
        {
            [Fact]
            public async Task RequestTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.GetAllAuthorizations("fake");

                connection.Received().GetAll<OrganizationCredential>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/credential-authorizations"));
            }

            [Fact]
            public async Task RequestTheCorrectUrlWithOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);
                
                await client.GetAllAuthorizations("fake", new ApiOptions() { PageCount = 1, PageSize = 1, StartPage = 1});

                connection.Received().GetAll<OrganizationCredential>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/credential-authorizations"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestTheCorrectUrlWithLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.GetAllAuthorizations("fake", "login");

                connection.Received().GetAll<OrganizationCredential>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/credential-authorizations?login=login"));
            }

            [Fact]
            public async Task RequestTheCorrectUrlWithLoginAndUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.GetAllAuthorizations("fake", "login", new ApiOptions() { PageCount = 1, PageSize = 1, StartPage = 1 });

                connection.Received().GetAll<OrganizationCredential>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/credential-authorizations?login=login"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizations(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAuthorizations(""));
            }

            [Fact]
            public async Task EnsuresNonNullArgumentsWithOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);
                
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizations(null, new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAuthorizations("", new ApiOptions()));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizations("asd", (ApiOptions)null));
            }

            [Fact]
            public async Task EnsuresNonNullArgumentsWithLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizations(null, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAuthorizations("", "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizations("asd", (string)null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAuthorizations("asd", ""));
            }

            [Fact]
            public async Task EnsuresNonNullArgumentsWithLoginAndOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizations(null, "asd", new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAuthorizations("", "asd", new ApiOptions()));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizations("asd", (string)null, new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAuthorizations("asd", "", new ApiOptions()));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizations("asd", "asd", null));
            }
        }
    }
}
