using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class IssuesEventsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new IssuesEventsClient(null));
            }
        }

        public class TheGetForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesEventsClient(connection);

                await client.GetAllForIssue("fake", "repo", 42);

                connection.Received().GetAll<EventInfo>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesEventsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForIssue("fake", "repo", 42, options);

                connection.Received().GetAll<EventInfo>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesEventsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, null));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesEventsClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesEventsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesEventsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesEventsClient(connection);

                client.Get("fake", "repo", 42);

                connection.Received().Get<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesEventsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
            }
        }
    }
}