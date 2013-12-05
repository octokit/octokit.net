using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class IssuesLabelsClientTests
    {
        public class TheGetForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetForIssue("fake", "repo", 42);

                connection.Received().GetAll<EventInfo>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetForRepository("fake", "repo");

                connection.Received().GetAll<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.Get("fake", "repo", "label");

                connection.Received().Get<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/labels/label"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "label"));
            }
        }
    }
}
