using System;
using System.Collections.Generic;
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
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Get("fake", "repo", 42);

                gitHubClient.Issue.Comment.Received().Get("fake", "repo", 42);
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

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.GetForRepository("fake", "repo");

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/comments", UriKind.Relative), null, null);
                }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository(null, "name"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("", "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository("owner", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("owner", ""));
            }
        }

        public class TheGetForIssueMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.GetForIssue("fake", "repo", 3);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/3/comments", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForIssue(null, "name", 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForIssue("", "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForIssue("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForIssue("owner", "", 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                const string newComment = "some title";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Create("fake", "repo", 1, newComment);

                gitHubClient.Issue.Comment.Received().Create("fake", "repo", 1, newComment);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", 1, "title"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", 1, "x"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, 1, "x"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", 1, "x"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", 1, null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                const string issueCommentUpdate = "Worthwhile update";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Update("fake", "repo", 42, issueCommentUpdate);

                gitHubClient.Issue.Comment.Received().Update("fake", "repo", 42, issueCommentUpdate);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update(null, "name", 42, "title"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Update("", "name", 42, "x"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("owner", null, 42, "x"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Update("owner", "", 42, "x"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("owner", "name", 42, null));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableIssueCommentsClient(null));
            }
        }
    }
}
