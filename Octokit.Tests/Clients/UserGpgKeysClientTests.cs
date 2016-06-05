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

                var expectedUri = ApiUrls.GpgKeys().ToString();
                client.GetAllForCurrent();

                connection.Received().GetAll<GpgKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<Dictionary<string, string>>(),
                    Arg.Is<string>(s => s == AcceptHeaders.GpgKeysPreview),
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

                var expectedUri = ApiUrls.GpgKeys(1).ToString();
                client.Get(1);

                connection.Received().Get<GpgKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<Dictionary<string, string>>(),
                    Arg.Is<string>(s => s == AcceptHeaders.GpgKeysPreview));
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

                var expectedUri = ApiUrls.GpgKeys().ToString();
                client.Create(new NewGpgKey("ABCDEFG"));

                connection.Received().Post<GpgKey>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>(),
                    Arg.Is<string>(s => s == AcceptHeaders.GpgKeysPreview));
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserGpgKeysClient(connection);

                var expectedUri = ApiUrls.GpgKeys().ToString();
                client.Create(new NewGpgKey("ABCDEFG"));

                connection.Received().Post<GpgKey>(
                    Arg.Any<Uri>(),
                    Arg.Is<NewGpgKey>(a =>
                        a.ArmoredPublicKey == "ABCDEFG"),
                    Arg.Any<string>());
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new UserGpgKeysClient(connection);

                var expectedUri = ApiUrls.GpgKeys(1).ToString();
                client.Delete(1);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<object>(),
                    Arg.Is<string>(s => s == AcceptHeaders.GpgKeysPreview));
            }
        }
    }
}
