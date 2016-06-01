using NSubstitute;
using Octokit.Reactive;
using System;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservablePullRequestReviewCommentReactionsClientTests
    {
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
                _client.PullRequestReviewComment.GetAll("fake", "repo", 42);
                _githubClient.Received().Reaction.PullRequestReviewComment.GetAll("fake", "repo", 42);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {

                Assert.Throws<ArgumentNullException>(() => _client.PullRequestReviewComment.Create(null, "name", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentException>(() => _client.PullRequestReviewComment.Create("", "name", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentNullException>(() => _client.PullRequestReviewComment.Create("owner", null, 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentException>(() => _client.PullRequestReviewComment.Create("owner", "", 1, new NewReaction(ReactionType.Heart)));
                Assert.Throws<ArgumentException>(() => _client.PullRequestReviewComment.Create("owner", "name", 1, null));
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

                client.PullRequestReviewComment.Create("fake", "repo", 1, newReaction);
                githubClient.Received().Reaction.PullRequestReviewComment.Create("fake", "repo", 1, newReaction);
            }
        }
    }
}
