﻿using System;
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
    public class OrganizationsClientTests
    {
        public class TheConstructor
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
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Organization>>();
                var orgsClient = new OrganizationsClient(client);

                orgsClient.Get("orgName");

                client.Received().Get(Arg.Is<Uri>(u => u.ToString() == "/orgs/orgName"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var orgs = new OrganizationsClient(Substitute.For<IApiConnection<Organization>>());

                AssertEx.Throws<ArgumentNullException>(async () => await orgs.Get(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Organization>>();
                var orgs = new OrganizationsClient(client);

                orgs.GetAll("username");

                client.Received().GetAll(Arg.Is<Uri>(u => u.ToString() == "/users/username/orgs"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var orgs = new OrganizationsClient(Substitute.For<IApiConnection<Organization>>());

                AssertEx.Throws<ArgumentNullException>(async () => await orgs.GetAll(null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Organization>>();
                var orgs = new OrganizationsClient(client);

                orgs.GetAllForCurrent();

                client.Received().GetAll(Arg.Is<Uri>(u => u.ToString() == "/user/orgs"), null);
            }
        }
    }
}
