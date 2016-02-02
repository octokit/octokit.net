using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

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
        public class TheDemoteMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Demote(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.Demote(""));
                Assert.Equal("login", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                client.Demote("auser");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "/users/auser/site_admin"));
            }
        }

        public class TheSuspendMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Suspend(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.Suspend(""));
                Assert.Equal("login", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                client.Suspend("auser");

                connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "/users/auser/suspended"));
            }
        }

        public class TheUnsuspendMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Unsuspend(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.Unsuspend(""));
                Assert.Equal("login", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                client.Unsuspend("auser");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "/users/auser/suspended"));
            }
        }
    }
}
