using NSubstitute;
using Octokit.Reactive;
using System;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableCommitCommentReactionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableCommitCommentReactionsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReactionsClient(gitHubClient);

                client.CommitComment.GetAll("fake", "repo", 42);

                gitHubClient.Received().Reaction.CommitComment.GetAll("fake", "repo", 42);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReactionsClient(gitHubClient);

                client.CommitComment.GetAll(1, 42);

                gitHubClient.Received().Reaction.CommitComment.GetAll(1, 42);
            }

            [Fact]
            public void EnsuresNotNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReactionsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.CommitComment.GetAll(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.CommitComment.GetAll("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.CommitComment.GetAll("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.CommitComment.GetAll("owner", "", 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReactionsClient(githubClient);
                var newReaction = new NewReaction(ReactionType.Confused);

                client.CommitComment.Create("fake", "repo", 1, newReaction);

                githubClient.Received().Reaction.CommitComment.Create("fake", "repo", 1, newReaction);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReactionsClient(githubClient);
                var newReaction = new NewReaction(ReactionType.Confused);

                client.CommitComment.Create(1, 1, newReaction);

                githubClient.Received().Reaction.CommitComment.Create(1, 1, newReaction);
            }

            [Fact]
            public void EnsuresNotNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReactionsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.CommitComment.Create(null, "name", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentNullException>(() => client.CommitComment.Create("owner", null, 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentNullException>(() => client.CommitComment.Create("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.CommitComment.Create(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.CommitComment.Create("", "name", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentException>(() => client.CommitComment.Create("owner", "", 1, new NewReaction(ReactionType.Heart)));
            }
        }
    }
}
