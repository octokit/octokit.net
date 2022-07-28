using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
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

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(gitHub);

                client.Get("fake", "repo", 42);

                gitHub.Received().Repository.Comment.Get("fake", "repo", 42);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(gitHub);

                client.Get(1, 42);

                gitHub.Received().Repository.Comment.Get(1, 42);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryCommentsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.GetAllForRepository("fake", "repo");
                githubClient.Connection.Received(1).Get<List<CommitComment>>(Arg.Is<Uri>(uri => uri.ToString() == "repos/fake/repo/comments"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.GetAllForRepository(1);
                githubClient.Connection.Received(1).Get<List<CommitComment>>(Arg.Is<Uri>(uri => uri.ToString() == "repositories/1/comments"),
                    Args.EmptyDictionary);
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
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllForRepository(1, options);
                githubClient.Connection.Received(1).Get<List<CommitComment>>(Arg.Is<Uri>(uri => uri.ToString() == "repositories/1/comments"),
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
            }
        }

        public class TheGetAllForCommitMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.GetAllForCommit("fake", "repo", "sha");
                githubClient.Connection.Received().Get<List<CommitComment>>(Arg.Is(new Uri("repos/fake/repo/commits/sha/comments", UriKind.Relative)),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.GetAllForCommit(1, "sha");
                githubClient.Connection.Received().Get<List<CommitComment>>(Arg.Is(new Uri("repositories/1/commits/sha/comments", UriKind.Relative)),
                    Args.EmptyDictionary);
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

                client.GetAllForCommit("fake", "repo", "sha", options);
                githubClient.Connection.Received().Get<List<CommitComment>>(Arg.Is(new Uri("repos/fake/repo/commits/sha/comments", UriKind.Relative)),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllForCommit(1, "sha", options);
                githubClient.Connection.Received().Get<List<CommitComment>>(Arg.Is(new Uri("repositories/1/commits/sha/comments", UriKind.Relative)),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(null, "name", "sha"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", null, "sha"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(null, "name", "sha", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", null, "sha", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", "sha", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(1, "sha", null));

                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("", "name", "sha"));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "", "sha"));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "name", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("", "name", "sha", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "", "sha", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", ApiOptions.None));

                Assert.Throws<ArgumentException>(() => client.GetAllForCommit(1, "", ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newComment = new NewCommitComment("body");

                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.Create("fake", "repo", "sha", newComment);

                githubClient.Repository.Comment.Received().Create("fake", "repo", "sha", newComment);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var newComment = new NewCommitComment("body");

                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.Create(1, "sha", newComment);

                githubClient.Repository.Comment.Received().Create(1, "sha", newComment);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", "sha", new NewCommitComment("body")).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, "sha", new NewCommitComment("body")).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null, new NewCommitComment("body")).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", "sha", null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null, new NewCommitComment("body")).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, "sha", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", "sha", new NewCommitComment("body")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", "sha", new NewCommitComment("body")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "name", "", new NewCommitComment("body")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create(1, "", new NewCommitComment("body")).ToTask());
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                const string issueCommentUpdate = "updated comment";

                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.Update("fake", "repo", 42, issueCommentUpdate);

                githubClient.Repository.Comment.Received().Update("fake", "repo", 42, issueCommentUpdate);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                const string issueCommentUpdate = "updated comment";

                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.Update(1, 42, issueCommentUpdate);

                githubClient.Repository.Comment.Received().Update(1, 42, issueCommentUpdate);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", 42, "updated comment"));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, 42, "updated comment"));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", 42, null));

                Assert.Throws<ArgumentNullException>(() => client.Update(1, 42, null));

                Assert.Throws<ArgumentException>(() => client.Update("", "name", 42, "updated comment"));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "", 42, "updated comment"));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.Delete("fake", "repo", 42);

                githubClient.Repository.Comment.Received().Delete("fake", "repo", 42);
            }

            [Fact]
            public void DeletesCorrectUrlWithRepositoryId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                client.Delete(1, 42);

                githubClient.Repository.Comment.Received().Delete(1, 42);
            }

            [Fact]
            public void EnsuresNonNullArgumentsOrEmpty()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 42));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 42));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 42));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 42));
            }
        }
    }
}
