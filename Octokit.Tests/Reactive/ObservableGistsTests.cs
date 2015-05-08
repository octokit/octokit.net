using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Reactive.Internal;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Reactive
{
    public class ObservableGistsTests
    {
        public class TheGetChildrenMethods
        {
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ObservableGistsClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllCommits(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllCommits(""));

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForks(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForks(""));
            }

            [Fact]
            public void RequestsCorrectGetCommitsUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(github);
                var expected = new Uri("gists/9257657/commits", UriKind.Relative);

                client.GetAllCommits("9257657");

                github.Connection.Received(1).Get<List<GistHistory>>(expected, Arg.Any<IDictionary<string, string>>(), null);
            }

            [Fact]
            public void RequestsCorrectGetForksUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableGistsClient(github);
                var expected = new Uri("gists/9257657/forks", UriKind.Relative);

                client.GetAllForks("9257657");

                github.Connection.Received(1).Get<List<GistFork>>(expected, Arg.Any<IDictionary<string, string>>(), null);
            }
        }
    }
}
