using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class PullRequestsClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                client.Get("fake", "repo", 42);

                connection.Received().Get<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PullRequestsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(() => client.Get(null, "", 1));
                await AssertEx.Throws<ArgumentException>(() => client.Get("", null, 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await client.GetForRepository("fake", "repo");

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                client.GetForRepository("fake", "repo", new PullRequestRequest { Head = "user:ref-head", Base = "fake_base_branch"  });

                connection.Received().GetAll<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["head"] == "user:ref-head"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newPullRequest = new NewPullRequest("some title", "branch:name", "branch:name");
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                client.Create("fake", "repo", newPullRequest);

                connection.Received().Post<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls"),
                    newPullRequest);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Create(null, "name", new NewPullRequest("title", "ref", "ref2")));
                await AssertEx.Throws<ArgumentException>(() =>
                    client.Create("", "name", new NewPullRequest("title", "ref", "ref2")));
                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Create("owner", null, new NewPullRequest("title", "ref", "ref2")));
                await AssertEx.Throws<ArgumentException>(() =>
                    client.Create("owner", "", new NewPullRequest("title", "ref", "ref2")));
                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Create("owner", "name", null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var pullRequestUpdate = new PullRequestUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                client.Update("fake", "repo", 42, pullRequestUpdate);

                connection.Received().Patch<PullRequest>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42"),
                    pullRequestUpdate);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Create(null, "name", new NewPullRequest("title", "ref", "ref2")));
                await AssertEx.Throws<ArgumentException>(() =>
                    client.Create("", "name", new NewPullRequest("title", "ref", "ref2")));
                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Create("owner", null, new NewPullRequest("title", "ref", "ref2")));
                await AssertEx.Throws<ArgumentException>(() =>
                    client.Create("owner", "", new NewPullRequest("title", "ref", "ref2")));
                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Create("owner", "name", null));
            }
        }

        public class TheMergeMethod 
        {
            [Fact]
            public void PutsToCorrectUrl() 
            {
                var mergePullRequest = new MergePullRequest("fake commit message");
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                client.Merge("fake", "repo", 42, mergePullRequest);

                connection.Received().Put<PullRequestMerge>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42/merge"),
                    mergePullRequest);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Merge(null, "name", 42, new MergePullRequest("message")));
                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Merge("owner", null, 42, new MergePullRequest("message")));
                await AssertEx.Throws<ArgumentNullException>(() =>
                    client.Merge("owner", "name", 42, null));
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
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(() => client.Merged(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(() => client.Merged("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(() => client.Merged(null, "", 1));
                await AssertEx.Throws<ArgumentException>(() => client.Merged("", null, 1));
            }
        }

        public class TheCommitsMethod 
        {
            [Fact]
            public async void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                client.Commits("fake", "repo", 42);
				
                connection.Received().GetAll<PullRequestCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/pulls/42/commits"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(() => client.Commits(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(() => client.Commits("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(() => client.Commits(null, "", 1));
                await AssertEx.Throws<ArgumentException>(() => client.Commits("", null, 1));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new PullRequestsClient(null));
            }
        }
    }
}
