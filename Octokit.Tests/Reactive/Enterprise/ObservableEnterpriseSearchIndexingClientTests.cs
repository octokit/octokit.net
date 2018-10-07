using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableEnterpriseSearchIndexingClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() =>
                    new ObservableEnterpriseSearchIndexingClient(null));
            }
        }

        public class TheQueueMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseSearchIndexingClient(github);

                client.Queue("org");
                github.Enterprise.SearchIndexing.Received(1).
                    Queue(Arg.Is("org"));

                client.Queue("org", "repo");
                github.Enterprise.SearchIndexing.Received(1).
                    Queue(Arg.Is("org"),
                          Arg.Is("repo"));
            }
        }

        public class TheQueueAllMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseSearchIndexingClient(github);

                client.QueueAll("org");
                github.Enterprise.SearchIndexing.Received(1).
                    QueueAll(Arg.Is("org"));
            }
        }

        public class TheQueueAllCodeMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseSearchIndexingClient(github);

                client.QueueAllCode("org");
                github.Enterprise.SearchIndexing.Received(1).
                    QueueAllCode(Arg.Is("org"));

                client.QueueAllCode("org", "repo");
                github.Enterprise.SearchIndexing.Received(1).
                    QueueAllCode(Arg.Is("org"),
                                 Arg.Is("repo"));
            }
        }

        public class TheQueueAllIssuesMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseSearchIndexingClient(github);

                client.QueueAllIssues("org");
                github.Enterprise.SearchIndexing.Received(1).
                    QueueAllIssues(Arg.Is("org"));

                client.QueueAllIssues("org", "repo");
                github.Enterprise.SearchIndexing.Received(1).
                    QueueAllIssues(Arg.Is("org"),
                                   Arg.Is("repo"));
            }
        }
    }
}
