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
                var client = Substitute.For<IApiConnection>();
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(client);

                await preReceiveEnvironmentsClient.GetAll();

                client.Received().GetAll<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments"),
                    null,
                    "application/vnd.github.eye-scream-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(client);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await preReceiveEnvironmentsClient.GetAll(options);

                client.Received().GetAll<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments"),
                    null,
                    "application/vnd.github.eye-scream-preview+json",
                    options);
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(connection);

                await preReceiveEnvironmentsClient.Get(1);

                connection.Received().Get<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1"),
                    null,
                    "application/vnd.github.eye-scream-preview+json");
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(client);
                var data = new NewPreReceiveEnvironment("name", "url");

                await preReceiveEnvironmentsClient.Create(data);

                client.Received().Post<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments"),
                    data,
                    "application/vnd.github.eye-scream-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new EnterprisePreReceiveEnvironmentsClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveEnvironment(null, "url"));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveEnvironment("", "url"));
                Assert.Throws<ArgumentNullException>(() => new NewPreReceiveEnvironment("name", null));
                Assert.Throws<ArgumentException>(() => new NewPreReceiveEnvironment("name", ""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Create(null));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(connection);
                var data = new UpdatePreReceiveEnvironment("name", "url");

                await preReceiveEnvironmentsClient.Edit(1, data);

                connection.Received().Patch<PreReceiveEnvironment>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1"),
                    data,
                    "application/vnd.github.eye-scream-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => new UpdatePreReceiveEnvironment(null, "url"));
                Assert.Throws<ArgumentException>(() => new UpdatePreReceiveEnvironment("", "url"));
                Assert.Throws<ArgumentNullException>(() => new UpdatePreReceiveEnvironment("name", null));
                Assert.Throws<ArgumentException>(() => new UpdatePreReceiveEnvironment("name", ""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => preReceiveEnvironmentsClient.Edit(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(connection);

                await preReceiveEnvironmentsClient.Delete(1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1"),
                    Arg.Any<object>(),
                    "application/vnd.github.eye-scream-preview+json");
            }
        }

        public class TheDownloadStatusMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(connection);

                await preReceiveEnvironmentsClient.DownloadStatus(1);

                connection.Received().Get<PreReceiveEnvironmentDownload>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1/downloads/latest"),
                    null,
                    "application/vnd.github.eye-scream-preview+json");
            }
        }

        public class TheTriggerDownloadMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var preReceiveEnvironmentsClient = new EnterprisePreReceiveEnvironmentsClient(connection);

                await preReceiveEnvironmentsClient.TriggerDownload(1);

                connection.Received().Post<PreReceiveEnvironmentDownload>(Arg.Is<Uri>(u => u.ToString() == "admin/pre-receive-environments/1/downloads"),
                    Arg.Any<object>(),
                    "application/vnd.github.eye-scream-preview+json");
            }
        }
    }
}
