using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class IssueCommentReactionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new IssueCommentReactionsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

                await client.GetAll("fake", "repo", 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments/42/reactions"), null, "application/vnd.github.squirrel-girl-preview", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", 42, options);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments/42/reactions"), null, "application/vnd.github.squirrel-girl-preview", options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

                await client.GetAll(1, 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/comments/42/reactions"), null, "application/vnd.github.squirrel-girl-preview", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, 42, options);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/comments/42/reactions"), null, "application/vnd.github.squirrel-girl-preview", options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1));
            }

            [Fact]
            public async Task EnsuresNonNullArgumentsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, 1, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

                client.Create("fake", "repo", 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments/1/reactions"), newReaction, "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

                client.Create(1, 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/comments/1/reactions"), newReaction, "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentReactionsClient(connection);

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
