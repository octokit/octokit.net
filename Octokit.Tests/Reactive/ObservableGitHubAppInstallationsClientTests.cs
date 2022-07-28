using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Octokit;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ObservableGitHubAppInstallationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableGitHubAppInstallationsClient(null));
            }
        }

        public class TheGetAllRepositoriesForCurrentMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppInstallationsClient(gitHubClient);

                client.GetAllRepositoriesForCurrent();

                connection.Received().Get<List<RepositoriesResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "installation/repositories"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void GetsFromCorrectUrllWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppInstallationsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1
                };

                client.GetAllRepositoriesForCurrent(options);

                connection.Received().Get<List<RepositoriesResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "installation/repositories"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 1
                            && x["per_page"] == "1"));
            }
        }

        public class TheGetAllRepositoriesForCurrentUserMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppInstallationsClient(gitHubClient);

                client.GetAllRepositoriesForCurrentUser(1234);

                connection.Received().Get<List<RepositoriesResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "user/installations/1234/repositories"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void GetsFromCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppInstallationsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1
                };

                client.GetAllRepositoriesForCurrentUser(1234, options);

                connection.Received().Get<List<RepositoriesResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "user/installations/1234/repositories"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 1
                            && x["per_page"] == "1"));
            }
        }
    }
}
