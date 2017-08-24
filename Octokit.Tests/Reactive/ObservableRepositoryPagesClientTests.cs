using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryPagesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryPagesClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                client.Get("fake", "repo");

                gitHubClient.Received().Repository.Page.Get("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                client.Get(1);

                gitHubClient.Received().Repository.Page.Get(1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", ""));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                client.GetAll("fake", "repo");

                gitHubClient.Received().Repository.Page.GetAll("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                client.GetAll(1);

                gitHubClient.Received().Repository.Page.GetAll(1);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll("fake", "repo", options);

                gitHubClient.Received().Repository.Page.GetAll("fake", "repo", options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll(1, options);

                gitHubClient.Received().Repository.Page.GetAll(1, options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "repo", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "repo", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheGetLatestBuildMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                client.GetLatest("fake", "repo");

                gitHubClient.Received().Repository.Page.GetLatest("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                client.GetLatest(1);

                gitHubClient.Repository.Page.Received().GetLatest(1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetLatest(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetLatest("owner", null));

                Assert.Throws<ArgumentException>(() => client.GetLatest("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetLatest("owner", ""));
            }
        }

        public class TheRequestPageBuildMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                client.RequestPageBuild("fake", "repo");

                gitHubClient.Received().Repository.Page.RequestPageBuild("fake", "repo");
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                client.RequestPageBuild(1);

                gitHubClient.Received().Repository.Page.RequestPageBuild(1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.RequestPageBuild(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.RequestPageBuild("owner", null));

                Assert.Throws<ArgumentException>(() => client.RequestPageBuild("", "name"));
                Assert.Throws<ArgumentException>(() => client.RequestPageBuild("owner", ""));
            }
        }
    }
}
