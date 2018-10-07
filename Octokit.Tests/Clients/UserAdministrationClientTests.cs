using System;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class UserAdministrationClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new UserAdministrationClient(null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                var expectedUri = "admin/users";
                client.Create(new NewUser("name", "email@company.com"));

                connection.Received().Post<User>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                client.Create(new NewUser("name", "email@company.com"));

                connection.Received().Post<User>(
                    Arg.Any<Uri>(),
                    Arg.Is<NewUser>(a =>
                        a.Login == "name" &&
                        a.Email == "email@company.com"));
            }
        }

        public class TheRenameMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Rename(null, new UserRename("newlogin")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Rename("login", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.Rename("", new UserRename()));
                Assert.Equal("login", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                var expectedUri = "admin/users/auser";
                client.Rename("auser", new UserRename());

                connection.Received().Patch<UserRenameResponse>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                client.Rename("auser", new UserRename("newlogin"));

                connection.Received().Patch<UserRenameResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<UserRename>(a =>
                        a.Login == "newlogin"));
            }
        }

        public class TheCreateImpersonationTokenMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateImpersonationToken(null, new NewImpersonationToken()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateImpersonationToken("login", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.CreateImpersonationToken("", new NewImpersonationToken()));
                Assert.Equal("login", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                var expectedUri = "admin/users/auser/authorizations";

                client.CreateImpersonationToken("auser", new NewImpersonationToken());

                connection.Received().Post<Authorization>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                string[] scopes = new string[] { "public-repo" };
                client.CreateImpersonationToken("auser", new NewImpersonationToken(scopes));

                connection.Received().Post<Authorization>(
                    Arg.Any<Uri>(),
                    Arg.Is<NewImpersonationToken>(a =>
                        a.Scopes.Count() == scopes.Length &&
                        a.Scopes.ToList().All(s => scopes.Contains(s)) &&
                        scopes.ToList().All(s => a.Scopes.Contains(s))));
            }
        }

        public class TheDeleteImpersonationTokenMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteImpersonationToken(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteImpersonationToken(""));
                Assert.Equal("login", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                var expectedUri = "admin/users/auser/authorizations";
                client.DeleteImpersonationToken("auser");

                connection.Connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

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

                var expectedUri = "users/auser/site_admin";
                client.Promote("auser");

                connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
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

                var expectedUri = "users/auser/site_admin";
                client.Demote("auser");

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
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

                var expectedUri = "users/auser/suspended";
                client.Suspend("auser");

                connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
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

                var expectedUri = "users/auser/suspended";
                client.Unsuspend("auser");

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheListAllPublicKeysMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                var expectedUri = "admin/keys";
                client.ListAllPublicKeys();

                connection.Received().GetAll<PublicKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserAdministrationClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(""));
                Assert.Equal("login", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                var expectedUri = "admin/users/auser";
                client.Delete("auser");

                connection.Connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheDeletePublicKeyMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserAdministrationClient(connection);

                var expectedUri = "admin/keys/1";
                client.DeletePublicKey(1);

                connection.Connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }
    }
}
