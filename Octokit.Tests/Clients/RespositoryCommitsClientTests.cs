using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RespositoryCommitsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new RepositoryCommitsClient(null));
            }
        }

        public class TheCompareMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await client.Compare("fake", "repo", "base", "head");

                connection.Received().Get<CompareResult>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/compare/base...head"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await client.Compare(1, "base", "head");

                connection.Received().Get<CompareResult>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/compare/base...head"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare(null, "name", "base", "head"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", null, "base", "head"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", "name", null, "head"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", "name", "base", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare(1, null, "head"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare(1, "base", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("", "name", "base", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "", "base", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "name", "", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "name", "base", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare(1, "", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare(1, "base", ""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await client.Get("fake", "repo", "reference");

                connection.Received().Get<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/reference"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await client.Get(1, "reference");

                connection.Received().Get<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/reference"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, ""));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await client.GetAll("fake", "repo");

                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits"), Args.EmptyDictionary, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits"), Args.EmptyDictionary, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", options);

                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits"), Args.EmptyDictionary, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, options);

                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits"), Args.EmptyDictionary, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlParameterized()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                var commitRequest = new CommitRequest
                {
                    Author = "author",
                    Sha = "sha",
                    Path = "path",
                    Since = null,
                    Until = null
                };

                await client.GetAll("fake", "repo", commitRequest);

                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits"),
                    Arg.Is<IDictionary<string, string>>(d => d["author"] == "author" && d["sha"] == "sha"
                        && d["path"] == "path" && !d.ContainsKey("since") && !d.ContainsKey("until")), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdParameterized()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                var commitRequest = new CommitRequest
                {
                    Author = "author",
                    Sha = "sha",
                    Path = "path",
                    Since = null,
                    Until = null
                };

                await client.GetAll(1, commitRequest);

                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits"),
                    Arg.Is<IDictionary<string, string>>(d => d["author"] == "author" && d["sha"] == "sha"
                        && d["path"] == "path" && !d.ContainsKey("since") && !d.ContainsKey("until")), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsParameterized()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                var commitRequest = new CommitRequest
                {
                    Author = "author",
                    Sha = "sha",
                    Path = "path",
                    Since = null,
                    Until = null
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", commitRequest, options);

                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits"), Arg.Is<IDictionary<string, string>>(d => d["author"] == "author" && d["sha"] == "sha"
                    && d["path"] == "path" && !d.ContainsKey("since") && !d.ContainsKey("until")), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptionsParameterized()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                var commitRequest = new CommitRequest
                {
                    Author = "author",
                    Sha = "sha",
                    Path = "path",
                    Since = null,
                    Until = null
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, commitRequest, options);

                connection.Received().GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits"), Arg.Is<IDictionary<string, string>>(d => d["author"] == "author" && d["sha"] == "sha"
                    && d["path"] == "path" && !d.ContainsKey("since") && !d.ContainsKey("until")), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", new CommitRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, new CommitRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", (CommitRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", new CommitRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, new CommitRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", new CommitRequest(), null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, (CommitRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, new CommitRequest(), null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", new CommitRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", new CommitRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", new CommitRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", new CommitRequest(), ApiOptions.None));
            }
        }

        public class TheGetSha1Method
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await client.GetSha1("fake", "repo", "ref");

                connection.Received().Get<string>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref"), null);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await client.GetSha1(1, "ref");

                connection.Received().Get<string>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1(null, "name", "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1("owner", null, "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("", "name", "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "", "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1(1, ""));
            }
        }

        public class ThePullRequestsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);
                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.PullRequests("fake", "repo", "ref", options);

                connection.Received().GetAll<CommitPullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/pulls"),
                    null, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);
                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.PullRequests(1, "ref", options);

                connection.Received().GetAll<CommitPullRequest>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/pulls"),
                    null, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.PullRequests(null, "name", "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.PullRequests("owner", null, "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.PullRequests("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.PullRequests(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.PullRequests("", "name", "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.PullRequests("owner", "", "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.PullRequests("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.PullRequests(1, ""));
            }
        }
    }
}
