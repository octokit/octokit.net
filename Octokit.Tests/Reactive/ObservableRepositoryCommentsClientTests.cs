using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;
using Octokit.Reactive;

namespace Octokit.Tests.Reactive
{
    class ObservableRepositoryCommentsClientTests
    {
        public class TheReactionMethod
        {
            private readonly IGitHubClient _githubClient;
            private readonly ObservableRepositoryCommentsClient _client;

            public TheReactionMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
                _client = new ObservableRepositoryCommentsClient(_githubClient);
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                NewCommitCommentReaction newCommitCommentReaction = new NewCommitCommentReaction(Reaction.Heart);

                var connection = Substitute.For<IApiConnection>();               

                _client.CreateReaction("fake", "repo", 1, newCommitCommentReaction);

                _githubClient.Connection.Received().Post<CommitCommentReaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/1/reactions"), Arg.Any<object>(), AcceptHeaders.Reactions, Arg.Any<string>());
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => _client.CreateReaction(null, "name", 1, new NewCommitCommentReaction(Reaction.Heart)));
                Assert.Throws<ArgumentException>(() => _client.CreateReaction("", "name", 1, new NewCommitCommentReaction(Reaction.Heart)));
                Assert.Throws<ArgumentNullException>(() => _client.CreateReaction("owner", null, 1, new NewCommitCommentReaction(Reaction.Heart)));
                Assert.Throws<ArgumentException>(() => _client.CreateReaction("owner", "", 1, new NewCommitCommentReaction(Reaction.Heart)));
            }
        }
    }
}