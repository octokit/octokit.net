using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryForksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ObservableRepositoryForksClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryForksClient(gitHubClient);

                client.GetAll("fake", "repo");

                gitHubClient.Received().Repository.Forks.GetAll("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryForksClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll("fake", "repo", options);

                gitHubClient.Received().Repository.Forks.GetAll("fake", "repo", options);
            }

            [Fact]
            public void RequestsCorrectUrlWithRequestParameters()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryForksClient(gitHubClient);

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Stargazers };

                client.GetAll("fake", "repo", repositoryForksListRequest);

                gitHubClient.Received().Repository.Forks.GetAll(
                    "fake", "repo", repositoryForksListRequest);
            }

            [Fact]
            public void RequestsCorrectUrlWithRequestParametersWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryForksClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Stargazers };

                client.GetAll("fake", "repo", repositoryForksListRequest, options);

                gitHubClient.Received().Repository.Forks.GetAll("fake", "name", repositoryForksListRequest, options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryForksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", new RepositoryForksListRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, new RepositoryForksListRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", new RepositoryForksListRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, new RepositoryForksListRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", new RepositoryForksListRequest(), null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", new RepositoryForksListRequest()));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", new RepositoryForksListRequest()));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", new RepositoryForksListRequest(), ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", new RepositoryForksListRequest(), ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryForksClient(gitHubClient);

                var newRepositoryFork = new NewRepositoryFork();

                client.Create("fake", "repo", newRepositoryFork);

                gitHubClient.Received().Repository.Forks.Create("fake", "repo", newRepositoryFork);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryForksClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewRepositoryFork()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewRepositoryFork()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewRepositoryFork()));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewRepositoryFork()));
            }
        }
    }
}
