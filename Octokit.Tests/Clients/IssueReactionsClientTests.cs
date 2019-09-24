using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class IssueReactionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new IssueReactionsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueReactionsClient(connection);

                await client.GetAll("fake", "repo", 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/reactions"), null, "application/vnd.github.squirrel-girl-preview", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueReactionsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", 42, options);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/reactions"), null, "application/vnd.github.squirrel-girl-preview", options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueReactionsClient(connection);

                await client.GetAll(1, 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/reactions"), null, "application/vnd.github.squirrel-girl-preview", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueReactionsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, 42, options);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/reactions"), null, "application/vnd.github.squirrel-girl-preview", options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Issue.GetAll(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Issue.GetAll("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Issue.GetAll("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Issue.GetAll("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Issue.GetAll("owner", "", 1));
            }

            [Fact]
            public async Task EnsuresNonNullArgumentsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Issue.GetAll(1, 1, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new IssueReactionsClient(connection);

                client.Create("fake", "repo", 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/1/reactions"), newReaction, "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new IssueReactionsClient(connection);

                client.Create(1, 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/1/reactions"), newReaction, "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Issue.Create(null, "name", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Issue.Create("owner", null, 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Issue.Create("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Issue.Create(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Issue.Create("", "name", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Issue.Create("owner", "", 1, new NewReaction(ReactionType.Heart)));
            }
        }
    }
}
