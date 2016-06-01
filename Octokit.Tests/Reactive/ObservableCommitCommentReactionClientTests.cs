using NSubstitute;
using Octokit.Reactive;
using System;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableCommitCommentReactionClientTests
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
            private readonly IGitHubClient _githubClient;
            private readonly IObservableReactionsClient _client;
            private const string owner = "owner";
            private const string name = "name";

            public TheGetAllMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
                _client = new ObservableReactionsClient(_githubClient);
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                _client.CommitComment.GetAll("fake", "repo", 42);
                _githubClient.Received().Reaction.CommitComment.GetAll("fake", "repo", 42);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {

                Assert.Throws<ArgumentNullException>(() => _client.CommitComment.Create(null, "name", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentException>(() => _client.CommitComment.Create("", "name", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentNullException>(() => _client.CommitComment.Create("owner", null, 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentException>(() => _client.CommitComment.Create("owner", "", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentNullException>(() => _client.CommitComment.Create("owner", "name", 1, null));
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
        }
    }
}
