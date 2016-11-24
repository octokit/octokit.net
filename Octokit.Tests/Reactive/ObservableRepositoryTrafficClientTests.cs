using NSubstitute;
using Octokit.Reactive;
using System;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryTrafficClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryTrafficClient(null));
            }
        }

        public class TheGetAllReferrersMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryTrafficClient(gitHubClient);

                client.GetReferrers("fake", "repo");

                gitHubClient.Received().Repository.Traffic.GetReferrers("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryTrafficClient(gitHubClient);

                client.GetReferrers(1);

                gitHubClient.Received().Repository.Traffic.GetReferrers(1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryTrafficClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetReferrers(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetReferrers("owner", null));

                Assert.Throws<ArgumentException>(() => client.GetReferrers("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetReferrers("owner", ""));
            }
        }

        public class TheGetAllPathsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryTrafficClient(gitHubClient);

                client.GetPaths("fake", "repo");

                gitHubClient.Received().Repository.Traffic.GetPaths("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryTrafficClient(gitHubClient);

                client.GetPaths(1);

                gitHubClient.Received().Repository.Traffic.GetPaths(1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryTrafficClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetPaths(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetPaths("owner", null));

                Assert.Throws<ArgumentException>(() => client.GetPaths("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetPaths("owner", ""));
            }
        }

        public class TheGetClonesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryTrafficClient(gitHubClient);
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                client.GetClones("fake", "repo", per);

                gitHubClient.Received().Repository.Traffic.GetClones("fake", "repo", per);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryTrafficClient(gitHubClient);
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                client.GetClones(1, per);

                gitHubClient.Received().Repository.Traffic.GetClones(1, per);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryTrafficClient(Substitute.For<IGitHubClient>());
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                Assert.Throws<ArgumentNullException>(() => client.GetClones(null, "name", per));
                Assert.Throws<ArgumentNullException>(() => client.GetClones("owner", null, per));
                Assert.Throws<ArgumentNullException>(() => client.GetClones("owner", "name", null));

                Assert.Throws<ArgumentException>(() => client.GetClones("", "name", per));
                Assert.Throws<ArgumentException>(() => client.GetClones("owner", "", per));

                Assert.Throws<ArgumentNullException>(() => client.GetClones(1, null));
            }
        }

        public class TheGetViewsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryTrafficClient(gitHubClient);
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                client.GetViews("fake", "repo", per);

                gitHubClient.Received().Repository.Traffic.GetViews("fake", "repo", per);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryTrafficClient(gitHubClient);
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                client.GetViews(1, per);

                gitHubClient.Received().Repository.Traffic.GetViews(1, per);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryTrafficClient(Substitute.For<IGitHubClient>());
                var per = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);

                Assert.Throws<ArgumentNullException>(() => client.GetViews(null, "name", per));
                Assert.Throws<ArgumentNullException>(() => client.GetViews("owner", null, per));
                Assert.Throws<ArgumentNullException>(() => client.GetViews("owner", "name", null));

                Assert.Throws<ArgumentException>(() => client.GetViews("", "name", per));
                Assert.Throws<ArgumentException>(() => client.GetViews("owner", "", per));

                Assert.Throws<ArgumentNullException>(() => client.GetViews(1, null));
            }
        }
    }
}
