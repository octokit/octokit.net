using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;
using Octokit.Clients;

namespace Octokit.Tests.Clients
{
    public class UserAdministrationClientTests
    {
       public class ThePromoteMethod
       {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Promote(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.Promote(""));
                Assert.Equal("login", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);
               
                client.Promote("auser");

                connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "/users/auser/site_admin"));
            }
        }
    }
}
