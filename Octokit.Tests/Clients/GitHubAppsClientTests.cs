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
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetCurrent();

                connection.Received().Get<GitHubApp>(Arg.Is<Uri>(u => u.ToString() == "app"), null, "application/vnd.github.machine-man-preview+json");
            }
        }

        public class TheGetAllInstallationsForCurrentMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllInstallationsForCurrent(null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetAllInstallationsForCurrent();

                connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations"), null, "application/vnd.github.machine-man-preview+json");
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

                connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations"), null, "application/vnd.github.machine-man-preview+json", options);
            }
        }

        public class TheGetInstallationMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetInstallation(123);

                connection.Received().Get<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations/123"), null, "application/vnd.github.machine-man-preview+json");
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

                connection.Received().Post<AccessToken>(Arg.Is<Uri>(u => u.ToString() == "installations/3141/access_tokens"), string.Empty, "application/vnd.github.machine-man-preview+json");
            }
        }

        public class TheGetAllInstallationsForUserMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetAllInstallationsForUser();

                connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "user/installations"), null, "application/vnd.github.machine-man-preview+json");
            }
        }

        public class TheGetInstallationsForUserMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetInstallationsForUser(options);

                connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "user/installations"), null, "application/vnd.github.machine-man-preview+json", options);
            }
        }
    }
}
