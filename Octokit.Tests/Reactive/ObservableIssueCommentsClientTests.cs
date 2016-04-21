﻿using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1).ToTask());
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/comments", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                var options=new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };
                client.GetAllForRepository("fake", "repo", options);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/comments", UriKind.Relative), Arg.Any<Dictionary<string, string>>(), null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None).ToTask());
            }
        }

        public class TheGetForIssueMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.GetAllForIssue("fake", "repo", 3);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/3/comments", UriKind.Relative), Args.EmptyDictionary, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                var options=new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };
                client.GetAllForIssue("fake", "repo", 3, options);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/3/comments", UriKind.Relative), Arg.Any<Dictionary<string, string>>(), null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("", "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("owner", "", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("", "name", 1, ApiOptions.None).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("owner", "", 1, ApiOptions.None).ToTask());
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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", 1, "title").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", 1, "x").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, 1, "x").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", 1, "x").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", 1, null).ToTask());
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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", 42, "title").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", 42, "x").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, 42, "x").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", 42, "x").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", 42, null).ToTask());
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableIssueCommentsClient(null));
            }
        }
    }
}
