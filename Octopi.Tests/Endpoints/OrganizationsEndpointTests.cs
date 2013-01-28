using System;
using System.Threading.Tasks;
using NSubstitute;
using Octopi.Endpoints;
using Octopi.Http;
using Octopi.Tests.Helpers;
using Xunit;

namespace Octopi.Tests.Endpoints
{
    /// <summary>
    /// Endpoint tests mostly just need to make sure they call the IApiClient with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiClientTests.cs.
    /// </summary>
    public class OrganizationsEndpointTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationsEndpoint(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var orgs = new OrganizationsEndpoint(Substitute.For<IApiClient<Organization>>());

                AssertEx.Throws<ArgumentNullException>(async () => await orgs.Get(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var orgs = new OrganizationsEndpoint(Substitute.For<IApiClient<Organization>>());

                AssertEx.Throws<ArgumentNullException>(async () => await orgs.GetAll(null));
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var client = Substitute.For<IApiClient<Organization>>();
                var orgs = new OrganizationsEndpoint(client);

                orgs.GetAll("username");

                client.Received().GetAll(Arg.Is<Uri>(u => u.ToString() == "/users/username/orgs"));
            }
        }
    }
}
