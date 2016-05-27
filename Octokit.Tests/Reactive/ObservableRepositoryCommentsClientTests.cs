using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryCommentsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryCommentsClient(null));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.GetAllForRepository("fake", "repo");
                githubClient.Connection.Received(1).Get<List<CommitComment>>(Arg.Is<Uri>(uri => uri.ToString() == "repos/fake/repo/comments"),
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0), null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllForRepository("fake", "repo", options);
                githubClient.Connection.Received(1).Get<List<CommitComment>>(Arg.Is<Uri>(uri => uri.ToString() == "repos/fake/repo/comments"),
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 2), null);
            }
        }

        public class TheGetAllForCommitMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.GetAllForCommit("fake", "repo", "sha1");
                githubClient.Received().Repository.Comment.GetAllForCommit("fake", "repo", "sha1", Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllForCommit("fake", "repo", "sha1", options);
                githubClient.Received().Repository.Comment.GetAllForCommit("fake", "repo", "sha1", options);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(null, null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(null, null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(null, null, "sha1", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(null, "name", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", null, null, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, Args.ApiOptions));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", "sha1", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, Args.ApiOptions));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", null, "sha1", Args.ApiOptions));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(null, "name", "sha1", Args.ApiOptions));

                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("", "", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("", "", "sha1", null));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("", "name", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "", "", null));

                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", Args.ApiOptions));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", "sha1", null));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", Args.ApiOptions));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "", "sha1", Args.ApiOptions));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("", "name", "sha1", Args.ApiOptions));
            }
        }
        public class TheReactionMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);
                var newReaction = new NewReaction(EnumReaction.Confused);

                client.CreateReaction("fake", "repo", 1, newReaction);
                githubClient.Received().Repository.Comment.CreateReaction("fake", "repo", 1, newReaction);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.CreateReaction(null, "name", 1, new NewReaction(EnumReaction.Heart)));
                Assert.Throws<ArgumentException>(() => client.CreateReaction("", "name", 1, new NewReaction(EnumReaction.Heart)));
                Assert.Throws<ArgumentNullException>(() => client.CreateReaction("owner", null, 1, new NewReaction(EnumReaction.Heart)));
                Assert.Throws<ArgumentException>(() => client.CreateReaction("owner", "", 1, new NewReaction(EnumReaction.Heart)));
            }
        }
    }
}
