using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class RepositoryBranchesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoryBranchesClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                await client.GetAll("owner", "name");

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/branches"), null, "application/vnd.github.loki-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                await client.GetAll(1);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches"), null, "application/vnd.github.loki-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("owner", "name", options);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/branches"), null, "application/vnd.github.loki-preview+json", options);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, options);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches"), null, "application/vnd.github.loki-preview+json", options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                await client.Get("owner", "repo", "branch");

                connection.Received()
                    .Get<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch"), null, "application/vnd.github.loki-preview+json");
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);

                await client.Get(1, "branch");

                connection.Received()
                    .Get<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch"), null, "application/vnd.github.loki-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "repo", ""));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new BranchUpdate();
                const string previewAcceptsHeader = "application/vnd.github.loki-preview+json";

                client.Edit("owner", "repo", "branch", update);

                connection.Received()
                    .Patch<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch"), Arg.Any<BranchUpdate>(), previewAcceptsHeader);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryBranchesClient(connection);
                var update = new BranchUpdate();
                const string previewAcceptsHeader = "application/vnd.github.loki-preview+json";

                client.Edit(1, "branch", update);

                connection.Received()
                    .Patch<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch"), Arg.Any<BranchUpdate>(), previewAcceptsHeader);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryBranchesClient(Substitute.For<IApiConnection>());
                var update = new BranchUpdate();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(null, "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", null, "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", "repo", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(1, null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("", "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("owner", "", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("owner", "repo", "", update));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Edit(1, "", update));
            }
        }
    }
}
