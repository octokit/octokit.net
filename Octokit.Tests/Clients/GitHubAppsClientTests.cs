using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class GitHubAppsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new GitHubAppsClient(null));
            }
        }

        public class TheGetCurrentMethod
        {
            [Fact]
            public void GetFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetCurrent();

                connection.Received().Get<GitHubApp>(Arg.Is<Uri>(u => u.ToString() == "app"), null, AcceptHeaders.MachineManPreview);
            }
        }

        public class TheCreateInstallationTokenMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                int fakeInstallationId = 3141;

                client.CreateInstallationToken(fakeInstallationId);

                connection.Received().Post<AccessToken>(Arg.Is<Uri>(u => u.ToString() == "installations/3141/access_tokens"), string.Empty, AcceptHeaders.MachineManPreview);
            }
        }

        public class TheGetAllInstallationsForCurrentMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllInstallationsForCurrent(null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetAllInstallationsForCurrent();

                connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations"), null, AcceptHeaders.MachineManPreview);
            }


            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllInstallationsForCurrent(options);

                connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations"), null, AcceptHeaders.MachineManPreview, options);
            }

        }

    }
}
