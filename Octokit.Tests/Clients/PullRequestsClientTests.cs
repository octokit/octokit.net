using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class PullRequestsClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Get("fake", "repo", 42);

                connection.Received().Get<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42"), Arg.Any<Dictionary<string, string>>(), "application/vnd.github.shadow-cat-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Get(1, 42);

                connection.Received().Get<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/42"), Arg.Any<Dictionary<string, string>>(), "application/vnd.github.shadow-cat-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PullRequestsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls"),
                    Arg.Any<Dictionary<string, string>>(), "application/vnd.github.shadow-cat-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls"),
                    Arg.Any<Dictionary<string, string>>(), "application/vnd.github.shadow-cat-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls"),
                    Arg.Any<Dictionary<string, string>>(), "application/vnd.github.shadow-cat-preview+json", options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository(1, options);

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls"),
                    Arg.Any<Dictionary<string, string>>(), "application/vnd.github.shadow-cat-preview+json", options);
            }

            [Fact]
            public async Task SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.GetAllForRepository("fake", "repo", new PullRequestRequest { Head = "user:ref-head", Base = "fake_base_branch" });

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-head"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"), "application/vnd.github.shadow-cat-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task SendsAppropriateParametersWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.GetAllForRepository(1, new PullRequestRequest { Head = "user:ref-head", Base = "fake_base_branch" });

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-head"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"), "application/vnd.github.shadow-cat-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task SendsAppropriateParametersWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository("fake", "repo", new PullRequestRequest { Head = "user:ref-head", Base = "fake_base_branch" }, options);

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-head"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"), "application/vnd.github.shadow-cat-preview+json", options);
            }

            [Fact]
            public async Task SendsAppropriateParametersWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository(1, new PullRequestRequest { Head = "user:ref-head", Base = "fake_base_branch" }, options);

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-head"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"), "application/vnd.github.shadow-cat-preview+json", options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PullRequestsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new PullRequestRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new PullRequestRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (PullRequestRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new PullRequestRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new PullRequestRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", new PullRequestRequest(), null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (PullRequestRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, new PullRequestRequest(), null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", new PullRequestRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", new PullRequestRequest()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", new PullRequestRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", new PullRequestRequest(), ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var newPullRequest = new NewPullRequest("some title", "branch:name", "branch:name");
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Create("fake", "repo", newPullRequest);

                connection.Received().Post<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls"),
                    newPullRequest, "application/vnd.github.shadow-cat-preview+json");
            }

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var newPullRequest = new NewPullRequest("some title", "branch:name", "branch:name");
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Create(1, newPullRequest);

                connection.Received().Post<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls"),
                    newPullRequest, "application/vnd.github.shadow-cat-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewPullRequest("title", "ref", "ref2")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewPullRequest("title", "ref", "ref2")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewPullRequest("title", "ref", "ref2")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewPullRequest("title", "ref", "ref2")));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var pullRequestUpdate = new PullRequestUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Update("fake", "repo", 42, pullRequestUpdate);

                connection.Received().Patch<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42"),
                    pullRequestUpdate, "application/vnd.github.shadow-cat-preview+json");
            }

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var pullRequestUpdate = new PullRequestUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Update(1, 42, pullRequestUpdate);

                connection.Received().Patch<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/42"),
                    pullRequestUpdate, "application/vnd.github.shadow-cat-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewPullRequest("title", "ref", "ref2")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewPullRequest("title", "ref", "ref2")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewPullRequest("title", "ref", "ref2")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewPullRequest("title", "ref", "ref2")));
            }
        }

        public class TheMergeMethod
        {
            [Fact]
            public void PutsToCorrectUrl()
            {
                var mergePullRequest = new MergePullRequest { CommitMessage = "fake commit message" };
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                client.Merge("fake", "repo", 42, mergePullRequest);

                connection.Received().Put<PullRequestMerge>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42/merge"),
                    mergePullRequest, null, "application/vnd.github.polaris-preview+json");
            }

            [Fact]
            public void PutsToCorrectUrlWithRepositoryId()
            {
                var mergePullRequest = new MergePullRequest { CommitMessage = "fake commit message" };
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                client.Merge(1, 42, mergePullRequest);

                connection.Received().Put<PullRequestMerge>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/42/merge"),
                    mergePullRequest, null, "application/vnd.github.polaris-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merge(null, "name", 42, new MergePullRequest { CommitMessage = "message" }));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merge("owner", null, 42, new MergePullRequest { CommitMessage = "message" }));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merge("owner", "name", 42, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merge(1, 42, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Merge("", "name", 42, new MergePullRequest { CommitMessage = "message" }));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Merge("owner", "", 42, new MergePullRequest { CommitMessage = "message" }));
            }
        }

        public class TheMergedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var conn = Substitute.For<IConnection>();
                var connection = Substitute.For<IApiConnection>();
                connection.Connection.Returns(conn);

                var client = new PullRequestsClient(connection);

                client.Merged("fake", "repo", 42);

                conn.Received().Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42/merge"), null, null);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var conn = Substitute.For<IConnection>();
                var connection = Substitute.For<IApiConnection>();
                connection.Connection.Returns(conn);

                var client = new PullRequestsClient(connection);

                client.Merged(1, 42);

                conn.Received().Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/42/merge"), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merged(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merged("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Merged("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Merged("owner", "", 1));
            }
        }

        public class TheCommitsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Commits("fake", "repo", 42);

                connection.Received()
                    .GetAll<PullRequestCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42/commits"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Commits(1, 42);

                connection.Received()
                    .GetAll<PullRequestCommit>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/42/commits"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Commits(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Commits("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Commits("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Commits("owner", "", 1));
            }
        }

        public class TheFilesMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Files("fake", "repo", 42);

                connection.Received()
                    .GetAll<PullRequestFile>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42/files"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.Files(1, 42);

                connection.Received()
                    .GetAll<PullRequestFile>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/pulls/42/files"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Files(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Files("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Files("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Files("owner", "", 1));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new PullRequestsClient(null));
            }
        }
    }
}
