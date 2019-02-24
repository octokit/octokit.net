using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableIssueCommentReactionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableIssueCommentReactionsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);

                client.GetAll("fake", "repo", 42);

                gitHubClient.Received().Reaction.IssueComment.GetAll("fake", "repo", 42, Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll("fake", "repo", 42, options);

                gitHubClient.Received().Reaction.IssueComment.GetAll("fake", "repo", 42, options);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);

                client.GetAll(1, 42);

                gitHubClient.Received().Reaction.IssueComment.GetAll(1, 42, Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll(1, 42, options);

                gitHubClient.Received().Reaction.IssueComment.GetAll(1, 42, options);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", 1));
            }

            [Fact]
            public void EnsuresArgumentsNotNullWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, 1, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);
                var newReaction = new NewReaction(ReactionType.Confused);

                client.Create("fake", "repo", 1, newReaction);

                gitHubClient.Received().Reaction.IssueComment.Create("fake", "repo", 1, newReaction);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);
                var newReaction = new NewReaction(ReactionType.Confused);

                client.Create(1, 1, newReaction);

                gitHubClient.Received().Reaction.IssueComment.Create(1, 1, newReaction);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentReactionsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", 1, new NewReaction(ReactionType.Heart)));
            }
        }
    }
}
