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
            [Fact]
            public void RequestsCorrectUrl()
            {
                NewCommitCommentReaction newCommitCommentReaction = new NewCommitCommentReaction(Reaction.Heart);

                var connection = Substitute.For<IApiConnection>();

                client.CreateReaction("fake", "repo", 1, newCommitCommentReaction);

                connection.Received().Post<CommitCommentReaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/1/reactions"), Arg.Any<object>(), AcceptHeaders.Reactions);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();

            }
        }
    }
}
