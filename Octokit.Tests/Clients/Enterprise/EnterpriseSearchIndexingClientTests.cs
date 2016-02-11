using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseSearchIndexingClientTests
    {
        public class TheQueueMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                string expectedUri = "staff/indexing_jobs";

                client.Queue("org");
                connection.Received().Post<SearchIndexingResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());

                client.Queue("org", "repo");
                connection.Received().Post<SearchIndexingResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                client.Queue("org");
                connection.Received().Post<SearchIndexingResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<SearchIndexTarget>(t =>
                        t.Target == "org"
                        ));

                client.Queue("org", "repo");
                connection.Received().Post<SearchIndexingResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<SearchIndexTarget>(t =>
                        t.Target == "org/repo"
                        ));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Queue(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Queue("org", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Queue(null, "repo"));
            }
        }

        public class TheQueueAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                string expectedUri = "staff/indexing_jobs";
                client.QueueAll("org");

                connection.Received().Post<SearchIndexingResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                client.QueueAll("org");
                connection.Received().Post<SearchIndexingResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<SearchIndexTarget>(t =>
                        t.Target == "org/*"
                        ));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueueAll(null));
            }
        }

        public class TheQueueAllCodeMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                string expectedUri = "staff/indexing_jobs";

                client.QueueAllCode("org");
                connection.Received().Post<SearchIndexingResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());

                client.QueueAllCode("org", "repo");
                connection.Received().Post<SearchIndexingResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                client.QueueAllCode("org");
                connection.Received().Post<SearchIndexingResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<SearchIndexTarget>(t =>
                        t.Target == "org/*/code"
                        ));

                client.QueueAllCode("org", "repo");
                connection.Received().Post<SearchIndexingResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<SearchIndexTarget>(t =>
                        t.Target == "org/repo/code"
                        ));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueueAllCode(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueueAllCode("org", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueueAllCode(null, "repo"));
            }
        }

        public class TheQueueAllIssuesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                string expectedUri = "staff/indexing_jobs";

                client.QueueAllIssues("org");
                connection.Received().Post<SearchIndexingResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());

                client.QueueAllIssues("org", "repo");
                connection.Received().Post<SearchIndexingResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                client.QueueAllIssues("org");
                connection.Received().Post<SearchIndexingResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<SearchIndexTarget>(t =>
                        t.Target == "org/*/issues"
                        ));

                client.QueueAllIssues("org", "repo");
                connection.Received().Post<SearchIndexingResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<SearchIndexTarget>(t =>
                        t.Target == "org/repo/issues"
                        ));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseSearchIndexingClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueueAllIssues(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueueAllIssues("org", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.QueueAllIssues(null, "repo"));
            }
        }
    }
}
