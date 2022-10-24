﻿using System;
using System.Runtime.InteropServices.ComTypes;
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

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42/reactions"), null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", 42, options);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42/reactions"), null, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                await client.GetAll(1, 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments/42/reactions"), null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, 42, options);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments/42/reactions"), null, options);
            }

            [Fact]
            public async Task EnsuresNotNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1));
            }

            [Fact]
            public async Task EnsuresNotNullArgumentsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

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
                var client = new CommitCommentReactionsClient(connection);

                client.Create("fake", "repo", 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/1/reactions"), newReaction);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new CommitCommentReactionsClient(connection);

                client.Create(1, 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments/1/reactions"), newReaction);
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
