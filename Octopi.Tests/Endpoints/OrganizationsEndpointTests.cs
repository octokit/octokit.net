using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octopi.Endpoints;
using Octopi.Http;
using Octopi.Tests.Helpers;
using Xunit;

namespace Octopi.Tests.Endpoints
{
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
                var orgs = new OrganizationsEndpoint(Substitute.For<IConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await orgs.Get(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var orgs = new OrganizationsEndpoint(Substitute.For<IConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await orgs.GetAll(null));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var links = new Dictionary<string, Uri>();
                var scopes = new List<string>();
                IResponse<List<Organization>> response = new ApiResponse<List<Organization>>
                {
                    ApiInfo = new ApiInfo(links, scopes, scopes, "", 1, 1),
                    BodyAsObject = new List<Organization>
                    {
                        new Organization { Login = "One" },
                        new Organization { Login = "Two" }
                    }
                };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<List<Organization>>(Args.Uri).Returns(Task.FromResult(response));
                var orgs = new OrganizationsEndpoint(connection);

                var organizations = await orgs.GetAll("username");

                organizations.Count.Should().Be(2);
                organizations.First().Login.Should().Be("One");
                organizations.Last().Login.Should().Be("Two");
                connection.Received()
                    .GetAsync<List<Organization>>(Arg.Is<Uri>(u => u.ToString() == "/users/username/orgs"));
            }
        }
    }
}
