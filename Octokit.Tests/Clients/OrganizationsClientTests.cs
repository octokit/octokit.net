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

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.GetAll("username");

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

                await client.GetAll("username", options);

                connection.Received().GetAll<Organization>(Arg.Is<Uri>(u => u.ToString() == "users/username/orgs"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll((string)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("username", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", ApiOptions.None));
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

                var request =  new OrganizationRequest(1);

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
    }
}
