using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class CommitStatusClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                await client.GetAll("fake", "repo", "sha");

                connection.Received()
                    .GetAll<CommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/statuses"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                await client.GetAll(1, "sha");

                connection.Received()
                    .GetAll<CommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/sha/statuses"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAll("fake", "repo", "sha", options);

                connection.Received()
                    .GetAll<CommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/statuses"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAll(1, "sha", options);

                connection.Received()
                    .GetAll<CommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/sha/statuses"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new CommitStatusClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", "sha"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, "sha"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", "sha", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, "sha", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", "sha", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, "sha", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", "sha"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", "sha"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "name", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", "sha", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", "sha", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "name", "", ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(1, "", ApiOptions.None));
            }
        }

        public class TheGetCombinedMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                await client.GetCombined("fake", "repo", "sha");

                connection.Received()
                    .Get<CombinedCommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/status"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                await client.GetCombined(1, "sha");

                connection.Received()
                    .Get<CombinedCommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/sha/status"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new CommitStatusClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCombined(null, "name", "sha"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCombined("owner", null, "sha"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCombined("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCombined(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetCombined("", "name", "sha"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetCombined("owner", "", "sha"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetCombined("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetCombined(1, ""));
            }
        }

        public class TheCreateMethodForUser
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                client.Create("owner", "repo", "sha", new NewCommitStatus { State = CommitState.Success });

                connection.Received().Post<CommitStatus>(Arg.Is<Uri>(u =>
                    u.ToString() == "repos/owner/repo/statuses/sha"),
                    Arg.Is<NewCommitStatus>(s => s.State == CommitState.Success));
            }

            [Fact]
            public void PostsToTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                client.Create(1, "sha", new NewCommitStatus { State = CommitState.Success });

                connection.Received().Post<CommitStatus>(Arg.Is<Uri>(u =>
                    u.ToString() == "repositories/1/statuses/sha"),
                    Arg.Is<NewCommitStatus>(s => s.State == CommitState.Success));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new CommitStatusClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", "sha", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, "sha", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null, new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", "sha", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null, new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, "sha", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", "sha", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", "sha", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "name", "", new NewCommitStatus()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create(1, "", new NewCommitStatus()));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new CommitStatusClient(null));
            }
        }
    }
}
