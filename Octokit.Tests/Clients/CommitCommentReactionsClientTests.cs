using System;
using System.Threading.Tasks;
using NSubstitute;
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
                var client = new CommitCommentReactionsClient(connection);

                await client.GetAll("fake", "repo", 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42/reactions"), "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                await client.GetAll(1, 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments/42/reactions"), "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNotNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                client.Create("fake", "repo", 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/1/reactions"), newReaction, "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                client.Create(1, 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments/1/reactions"), newReaction, "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNotNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", 1, new NewReaction(ReactionType.Heart)));
            }
        }
    }
}
