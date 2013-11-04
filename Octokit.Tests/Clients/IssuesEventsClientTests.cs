using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class IssuesEventsClientTests
    {
        public class TheGetForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesEventsClient(connection);

                await client.GetForIssue("fake", "repo", 42);

                connection.Received().GetAll<EventInfo>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/events"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesEventsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesEventsClient(connection);

                await client.GetForRepository("fake", "repo");

                connection.Received().GetAll<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesEventsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, 1));
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

                connection.Received().Get<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events/42"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesEventsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, 1));
            }

        }
    }
}