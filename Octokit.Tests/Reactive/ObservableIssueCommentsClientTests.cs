using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableIssueCommentsClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Get("fake", "repo", 42);

                gitHubClient.Issue.Comment.Received().Get("fake", "repo", 42);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Get(1, 42);

                gitHubClient.Issue.Comment.Received().Get(1, 42);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssueCommentsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/comments", UriKind.Relative),
                    Arg.Any<IDictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.GetAllForRepository(1);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repositories/1/issues/comments", UriKind.Relative),
                    Arg.Any<IDictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                var request = new IssueCommentRequest()
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2016, 11, 23, 11, 11, 11, 00, new TimeSpan()),
                    Sort = IssueCommentSort.Updated
                };
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };

                client.GetAllForRepository("fake", "repo", request, options);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/comments", UriKind.Relative),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["direction"] == "desc"
                        && d["since"] == "2016-11-23T11:11:11Z"
                        && d["sort"] == "updated"),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                var request = new IssueCommentRequest()
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2016, 11, 23, 11, 11, 11, 00, new TimeSpan()),
                    Sort = IssueCommentSort.Updated
                };
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };

                client.GetAllForRepository(1, request, options);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repositories/1/issues/comments", UriKind.Relative),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["direction"] == "desc"
                        && d["since"] == "2016-11-23T11:11:11Z"
                        && d["sort"] == "updated"),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", request: null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", options: null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, request: null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, options: null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
            }
        }

        public class TheGetForIssueMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.GetAllForIssue("fake", "repo", 3);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/3/comments", UriKind.Relative),
                    Arg.Any<IDictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.GetAllForIssue(1, 3);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repositories/1/issues/3/comments", UriKind.Relative), Arg.Any<IDictionary<string, string>>(), "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithIssueCommentRequest()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                var request = new IssueCommentRequest()
                {
                    Since = new DateTimeOffset(2016, 11, 23, 11, 11, 11, 00, new TimeSpan()),
                };

                client.GetAllForIssue("fake", "repo", 3, request);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/3/comments", UriKind.Relative),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 3
                         && d["since"] == "2016-11-23T11:11:11Z"),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithIssueCommentRequest()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                var request = new IssueCommentRequest()
                {
                    Since = new DateTimeOffset(2016, 11, 23, 11, 11, 11, 00, new TimeSpan()),
                };

                client.GetAllForIssue(1, 3, request);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repositories/1/issues/3/comments", UriKind.Relative),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                                && d["since"] == "2016-11-23T11:11:11Z"),
                            "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };

                client.GetAllForIssue("fake", "repo", 3, options);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repos/fake/repo/issues/3/comments", UriKind.Relative),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 4),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };

                client.GetAllForIssue(1, 3, options);

                gitHubClient.Connection.Received(1).Get<List<IssueComment>>(
                    new Uri("repositories/1/issues/3/comments", UriKind.Relative),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4),
                            "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, options: null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, request: null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, null, ApiOptions.None));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(1, 1, options: null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(1, 1, request: null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(1, 1, null, ApiOptions.None));

                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("", "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("owner", "", 1, ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                const string newComment = "some title";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Create("fake", "repo", 1, newComment);

                gitHubClient.Issue.Comment.Received().Create("fake", "repo", 1, newComment);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                const string newComment = "some title";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Create(1, 1, newComment);

                gitHubClient.Issue.Comment.Received().Create(1, 1, newComment);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", 1, "x"));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, 1, "x"));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", 1, "x"));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", 1, "x"));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                const string issueCommentUpdate = "Worthwhile update";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Update("fake", "repo", 42, issueCommentUpdate);

                gitHubClient.Issue.Comment.Received().Update("fake", "repo", 42, issueCommentUpdate);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                const string issueCommentUpdate = "Worthwhile update";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Update(1, 42, issueCommentUpdate);

                gitHubClient.Issue.Comment.Received().Update(1, 42, issueCommentUpdate);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", 42, "title"));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, 42, "x"));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", 42, null));

                Assert.Throws<ArgumentNullException>(() => client.Update(1, 42, null));

                Assert.Throws<ArgumentException>(() => client.Update("", "name", 42, "x"));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "", 42, "x"));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Delete("fake", "repo", 42);

                gitHubClient.Issue.Comment.Received().Delete("fake", "repo", 42);
            }

            [Fact]
            public void DeletesCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                client.Delete(1, 42);

                gitHubClient.Issue.Comment.Received().Delete(1, 42);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNullOrEmpty()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssueCommentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 42));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 42));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 42));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 42));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableIssueCommentsClient(null));
            }
        }
    }
}
