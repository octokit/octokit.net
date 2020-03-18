using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class UserGpgKeysClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new UserGpgKeysClient(null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserGpgKeysClient(connection);

                var expectedUri = "user/gpg_keys";
                client.GetAllForCurrent();

                connection.Received().GetAll<GpgKey>(
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
                var client = new UserGpgKeysClient(connection);

                var expectedUri = "user/gpg_keys/1";
                client.Get(1);

                connection.Received().Get<GpgKey>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArgument()
            {
                var client = new UserGpgKeysClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserGpgKeysClient(connection);

                var expectedUri = "user/gpg_keys";
                client.Create(new NewGpgKey("ABCDEFG"));

                connection.Received().Post<GpgKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserGpgKeysClient(connection);

                var expectedUri = "user/gpg_keys";
                client.Create(new NewGpgKey("ABCDEFG"));

                connection.Received().Post<GpgKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Is<NewGpgKey>(a => a.ArmoredPublicKey == "ABCDEFG"));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserGpgKeysClient(connection);

                var expectedUri = "user/gpg_keys/1";
                client.Delete(1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }
    }
}
