using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableIssueCommentsClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void GetsFromClientIssueComment()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(connection);

                client.Get("fake", "repo", 42);

                connection.Issue.Comment.Received().Get("fake", "repo", 42);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssueCommentsClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", 1));
            }

        }
    }
}
