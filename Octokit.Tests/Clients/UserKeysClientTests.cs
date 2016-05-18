using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class UserKeysClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserKeysClient(connection);

                var expectedUri = "user/keys";
                client.GetAllForCurrent();

                connection.Received().GetAll<PublicKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<ApiOptions>());
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserKeysClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("fake", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyString()
            {
                var client = new UserKeysClient(Substitute.For<IApiConnection>());
                var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
                Assert.Equal("userName", exception.ParamName);
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserKeysClient(connection);

                var expectedUri = "users/auser/keys";
                client.GetAll("auser");

                connection.Received().GetAll<PublicKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<ApiOptions>());
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserKeysClient(connection);

                var expectedUri = "user/keys/1";
                client.Get(1);

                connection.Received().Get<PublicKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new UserKeysClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserKeysClient(connection);

                var expectedUri = "user/keys";
                client.Create(new NewPublicKey("title", "ABCDEFG"));

                connection.Received().Post<PublicKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserKeysClient(connection);

                client.Create(new NewPublicKey("title", "ABCDEFG"));

                connection.Received().Post<PublicKey>(
                    Arg.Any<Uri>(),
                    Arg.Is<NewPublicKey>(a =>
                        a.Title == "title" &&
                        a.Key == "ABCDEFG"));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserKeysClient(connection);

                var expectedUri = "user/keys/1";
                client.Delete(1);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new UserEmailsClient(null));
            }
        }
    }
}
