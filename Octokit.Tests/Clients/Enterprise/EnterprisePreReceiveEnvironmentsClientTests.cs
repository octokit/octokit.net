using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterprisePreReceiveEnvironmentsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new EnterprisePreReceiveEnvironmentsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveEnvironmentsClient(connection);

                await client.GetAll();

                connection.Received().GetAll<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments"),
                    null,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveEnvironmentsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAll(options);

                connection.Received().GetAll<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments"),
                    null,
                    options);
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveEnvironmentsClient(connection);

                await client.Get(1);

                connection.Received().Get<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1"),
                    null);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveEnvironmentsClient(connection);
                var data = new NewPreReceiveEnvironment("name", "url");

                await client.Create(data);

                connection.Received().Post<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments"),
                    data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnterprisePreReceiveEnvironmentsClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveEnvironment(null, "url"));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveEnvironment("", "url"));
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveEnvironment("name", null));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveEnvironment("name", ""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveEnvironmentsClient(connection);
                var data = new UpdatePreReceiveEnvironment
                {
                    Name = "name",
                    ImageUrl = "url"
                };

                await client.Edit(1, data);

                connection.Received().Patch<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1"),
                    data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnterprisePreReceiveEnvironmentsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveEnvironmentsClient(connection);

                await client.Delete(1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1"),
                    Arg.Any<object>());
            }
        }

        public class TheDownloadStatusMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveEnvironmentsClient(connection);

                await client.DownloadStatus(1);

                connection.Received().Get<PreReceiveEnvironmentDownload>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1/downloads/latest"),
                    null);
            }
        }

        public class TheTriggerDownloadMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterprisePreReceiveEnvironmentsClient(connection);

                await client.TriggerDownload(1);

                connection.Received().Post<PreReceiveEnvironmentDownload>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1/downloads"),
                    Arg.Any<object>());
            }
        }
    }
}
