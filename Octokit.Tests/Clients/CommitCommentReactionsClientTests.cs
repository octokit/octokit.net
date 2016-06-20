using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class CommitCommentReactionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new CommitCommentReactionsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                client.CommitComment.GetAll("fake", "repo", 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42/reactions"), "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CommitComment.Create(null, "name", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CommitComment.Create("", "name", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CommitComment.Create("owner", null, 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CommitComment.Create("owner", "", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CommitComment.Create("owner", "name", 1, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                client.CommitComment.Create("fake", "repo", 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/1/reactions"), Arg.Any<object>(), "application/vnd.github.squirrel-girl-preview");
            }
        }
    }
}
