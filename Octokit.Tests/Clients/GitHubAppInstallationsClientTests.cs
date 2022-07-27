using System;
using System.Linq;
using NSubstitute;
using Octokit;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class GitHubAppInstallationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new GitHubAppInstallationsClient(null));
            }
        }

        public class TheGetAllRepositoriesForCurrentMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppInstallationsClient(connection);

                client.GetAllRepositoriesForCurrent();

                connection.Received().GetAll<RepositoriesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "installation/repositories"),
                    null,
                    Args.ApiOptions);
            }

            [Fact]
            public void GetsFromCorrectUrllWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppInstallationsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllRepositoriesForCurrent(options);

                connection.Received().GetAll<RepositoriesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "installation/repositories"),
                    null,
                    options);
            }
        }

        public class TheGetAllRepositoriesForCurrentUserMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppInstallationsClient(connection);

                client.GetAllRepositoriesForCurrentUser(1234);

                connection.Received().GetAll<RepositoriesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "user/installations/1234/repositories"),
                    null,
                    Args.ApiOptions);
            }

            [Fact]
            public void GetsFromCorrectUrllWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppInstallationsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllRepositoriesForCurrentUser(1234, options);

                connection.Received().GetAll<RepositoriesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "user/installations/1234/repositories"),
                    null,
                    options);
            }
        }
    }
}
