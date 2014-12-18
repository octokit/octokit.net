using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class RepositoriesClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoriesClient(null));
            }
        }

        public class TheCreateMethodForUser
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create(new NewRepository { Name = null }));
            }

            [Fact]
            public void UsesTheUserReposUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Create(new NewRepository { Name = "aName" });

                connection.Received().Post<Repository>(Arg.Is<Uri>(u => u.ToString() == "user/repos"), Arg.Any<NewRepository>());
            }

            [Fact]
            public void TheNewRepositoryDescription()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepository = new NewRepository { Name = "aName" };

                client.Create(newRepository);

                connection.Received().Post<Repository>(Args.Uri, newRepository);
            }

            [Fact]
            public async Task ThrowsRepositoryExistsExceptionWhenRepositoryExistsForCurrentUser()
            {
                var newRepository = new NewRepository { Name = "aName" };
                var response = Substitute.For<IResponse>();
                response.StatusCode.Returns((HttpStatusCode)422);
                response.Body.Returns(@"{""message"":""Validation Failed"",""documentation_url"":"
                    + @"""http://developer.github.com/v3/repos/#create"",""errors"":[{""resource"":""Repository"","
                    + @"""code"":""custom"",""field"":""name"",""message"":""name already exists on this account""}]}");
                var credentials = new Credentials("haacked", "pwd");
                var connection = Substitute.For<IApiConnection>();
                connection.Connection.BaseAddress.Returns(GitHubClient.GitHubApiUrl);
                connection.Connection.Credentials.Returns(credentials);
                connection.Post<Repository>(Args.Uri, newRepository)
                    .Returns<Task<Repository>>(_ => { throw new ApiValidationException(response); });
                var client = new RepositoriesClient(connection);

                var exception = await AssertEx.Throws<RepositoryExistsException>(
                    async () => await client.Create(newRepository));

                Assert.False(exception.OwnerIsOrganization);
                Assert.Null(exception.Owner);
                Assert.Equal("aName", exception.RepositoryName);
                Assert.Null(exception.ExistingRepositoryWebUrl);
            }

            [Fact]
            public async Task ThrowsExceptionWhenPrivateRepositoryQuotaExceeded()
            {
                var newRepository = new NewRepository { Name = "aName", Private = true };
                var response = Substitute.For<IResponse>();
                response.StatusCode.Returns((HttpStatusCode)422);
                response.Body.Returns(@"{""message"":""Validation Failed"",""documentation_url"":"
                    + @"""http://developer.github.com/v3/repos/#create"",""errors"":[{""resource"":""Repository"","
                    + @"""code"":""custom"",""field"":""name"",""message"":"
                    + @"""name can't be private. You are over your quota.""}]}");
                var credentials = new Credentials("haacked", "pwd");
                var connection = Substitute.For<IApiConnection>();
                connection.Connection.BaseAddress.Returns(GitHubClient.GitHubApiUrl);
                connection.Connection.Credentials.Returns(credentials);
                connection.Post<Repository>(Args.Uri, newRepository)
                    .Returns<Task<Repository>>(_ => { throw new ApiValidationException(response); });
                var client = new RepositoriesClient(connection);

                var exception = await AssertEx.Throws<PrivateRepositoryQuotaExceededException>(
                    async () => await client.Create(newRepository));

                Assert.NotNull(exception);
            }
        }

        public class TheCreateMethodForOrganization
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, new NewRepository { Name = "aName" }));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("aLogin", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("aLogin", new NewRepository { Name = null }));
            }

            [Fact]
            public async Task UsesTheOrganizatinosReposUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.Create("theLogin", new NewRepository { Name = "aName" });

                connection.Received().Post<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/theLogin/repos"),
                    Args.NewRepository);
            }

            [Fact]
            public async Task TheNewRepositoryDescription()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepository = new NewRepository { Name = "aName" };

                await client.Create("aLogin", newRepository);

                connection.Received().Post<Repository>(Args.Uri, newRepository);
            }

            [Fact]
            public async Task ThrowsRepositoryExistsExceptionWhenRepositoryExistsForSpecifiedOrg()
            {
                var newRepository = new NewRepository { Name = "aName" };
                var response = Substitute.For<IResponse>();
                response.StatusCode.Returns((HttpStatusCode)422);
                response.Body.Returns(@"{""message"":""Validation Failed"",""documentation_url"":"
                    + @"""http://developer.github.com/v3/repos/#create"",""errors"":[{""resource"":""Repository"","
                    + @"""code"":""custom"",""field"":""name"",""message"":""name already exists on this account""}]}");
                var connection = Substitute.For<IApiConnection>();
                connection.Connection.BaseAddress.Returns(GitHubClient.GitHubApiUrl);
                connection.Post<Repository>(Args.Uri, newRepository)
                    .Returns<Task<Repository>>(_ => { throw new ApiValidationException(response); });
                var client = new RepositoriesClient(connection);

                var exception = await AssertEx.Throws<RepositoryExistsException>(
                    async () => await client.Create("illuminati", newRepository));

                Assert.True(exception.OwnerIsOrganization);
                Assert.Equal("illuminati", exception.Owner);
                Assert.Equal("aName", exception.RepositoryName);
                Assert.Equal(new Uri("https://github.com/illuminati/aName"), exception.ExistingRepositoryWebUrl);
                Assert.Equal("There is already a repository named 'aName' in the organization 'illuminati'.",
                    exception.Message);
            }

            [Fact]
            public async Task ThrowsValidationException()
            {
                var newRepository = new NewRepository { Name = "aName" };
                var response = Substitute.For<IResponse>();
                response.StatusCode.Returns((HttpStatusCode)422);
                response.Body.Returns(@"{""message"":""Validation Failed"",""documentation_url"":"
                    + @"""http://developer.github.com/v3/repos/#create"",""errors"":[]}");
                var connection = Substitute.For<IApiConnection>();
                connection.Connection.BaseAddress.Returns(GitHubClient.GitHubApiUrl);
                connection.Post<Repository>(Args.Uri, newRepository)
                    .Returns<Task<Repository>>(_ => { throw new ApiValidationException(response); });
                var client = new RepositoriesClient(connection);

                var exception = await AssertEx.Throws<ApiValidationException>(
                    async () => await client.Create("illuminati", newRepository));

                Assert.Null(exception as RepositoryExistsException);
            }

            [Fact]
            public async Task ThrowsRepositoryExistsExceptionForEnterpriseInstance()
            {
                var newRepository = new NewRepository { Name = "aName" };
                var response = Substitute.For<IResponse>();
                response.StatusCode.Returns((HttpStatusCode)422);
                response.Body.Returns(@"{""message"":""Validation Failed"",""documentation_url"":"
                    + @"""http://developer.github.com/v3/repos/#create"",""errors"":[{""resource"":""Repository"","
                    + @"""code"":""custom"",""field"":""name"",""message"":""name already exists on this account""}]}");
                var connection = Substitute.For<IApiConnection>();
                connection.Connection.BaseAddress.Returns(new Uri("https://example.com"));
                connection.Post<Repository>(Args.Uri, newRepository)
                    .Returns<Task<Repository>>(_ => { throw new ApiValidationException(response); });
                var client = new RepositoriesClient(connection);

                var exception = await AssertEx.Throws<RepositoryExistsException>(
                    async () => await client.Create("illuminati", newRepository));

                Assert.Equal("aName", exception.RepositoryName);
                Assert.Equal(new Uri("https://example.com/illuminati/aName"), exception.ExistingRepositoryWebUrl);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete(null, "aRepoName"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("anOwner", null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.Delete("theOwner", "theRepoName");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/theOwner/theRepoName"));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Get("fake", "repo");

                connection.Received().Get<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllForCurrent();

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "user/repos"));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllForUser("username");

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "users/username/repos"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => reposEndpoint.GetAllForUser(null));
            }
        }

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllForOrg("orgname");

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgname/repos"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => reposEndpoint.GetAllForOrg(null));
            }
        }

        public class TheGetReadmeMethod
        {
            [Fact]
            public async Task ReturnsReadme()
            {
                string encodedContent = Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello world"));
                var readmeInfo = new ReadmeResponse
                {
                    Content = encodedContent,
                    Encoding = "base64",
                    Name = "README.md",
                    Url = "https://github.example.com/readme.md",
                    HtmlUrl = "https://github.example.com/readme"
                };
                var connection = Substitute.For<IApiConnection>();
                connection.Get<ReadmeResponse>(Args.Uri, null).Returns(Task.FromResult(readmeInfo));
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult("<html>README</html>"));
                var reposEndpoint = new RepositoriesClient(connection);

                var readme = await reposEndpoint.GetReadme("fake", "repo");

                Assert.Equal("README.md", readme.Name);
                connection.Received().Get<ReadmeResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/readme"),
                    null);
                connection.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme.md"),
                    null);
                var htmlReadme = await readme.GetHtmlContent();
                Assert.Equal("<html>README</html>", htmlReadme);
                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme.md"), null);
            }
        }

        public class TheGetReadmeHtmlMethod
        {
            [Fact]
            public async Task ReturnsReadmeHtml()
            {
                var connection = Substitute.For<IApiConnection>();
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult("<html>README</html>"));
                var reposEndpoint = new RepositoriesClient(connection);

                var readme = await reposEndpoint.GetReadmeHtml("fake", "repo");

                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/readme"), null);
                Assert.Equal("<html>README</html>", readme);
            }
        }

        public class TheGetAllBranchesMethod
        {
            [Fact]
            public void ReturnsBranches()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllBranches("owner", "name");

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/branches"));
            }

            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllBranches(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllBranches("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllBranches("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllBranches("owner", ""));
            }
        }

        public class TheGetAllContributorsMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllContributors("owner", "name");

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/contributors"), Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContributors("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContributors("owner", ""));
            }
        }

        public class TheGetAllLanguagesMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllLanguages("owner", "name");

                connection.Received()
                    .Get<IDictionary<string, long>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/languages"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllLanguages(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllLanguages("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllLanguages("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllLanguages("owner", ""));
            }
        }

        public class TheGetAllTeamsMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllTeams("owner", "name");

                connection.Received()
                    .GetAll<Team>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/teams"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTeams(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTeams("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTeams("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTeams("owner", ""));
            }
        }

        public class TheGetAllTagsMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllTags("owner", "name");

                connection.Received()
                    .GetAll<RepositoryTag>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/tags"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTags(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTags("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTags("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTags("owner", ""));
            }
        }

        public class TheGetBranchMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetBranch("owner", "repo", "branch");

                connection.Received()
                    .Get<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranch(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranch("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranch("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetBranch("", "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetBranch("owner", "", "branch"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetBranch("owner", "repo", ""));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void PatchesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var update = new RepositoryUpdate();

                client.Edit("owner", "repo", update);

                connection.Received()
                    .Patch<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo"), Arg.Any<RepositoryUpdate>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());
                var update = new RepositoryUpdate();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(null, "repo", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("", "repo", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Edit("owner", "", update));
            }
        }

        public class TheCompareMethod
        {
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new RepositoryCommitsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare(null, "repo", "base", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("", "repo", "base", "head"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", null, "base", "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "", "base", "head"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", "repo", null, "head"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "repo", "", "head"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", "repo", "base", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "repo", "base", ""));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();

                var client = new RepositoryCommitsClient(connection);

                client.Compare("owner", "repo", "base", "head");

                connection.Received()
                    .Get<CompareResult>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/compare/base...head"), null);
            }

            [Fact]
            public void EncodesUrl()
            {
                var connection = Substitute.For<IApiConnection>();

                var client = new RepositoryCommitsClient(connection);

                client.Compare("owner", "repo", "base", "shiftkey/my-cool-branch");

                connection.Received()
                    .Get<CompareResult>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/compare/base...shiftkey%2Fmy-cool-branch"), null);
            }
        }

        public class TheGetCommitMethod
        {
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new RepositoryCommitsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", "reference"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "reference"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "repo", ""));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                client.Get("owner", "name", "reference");

                connection.Received()
                    .Get<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/commits/reference"), null);
            }
        }

        public class TheGetAllCommitsMethod
        {
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new RepositoryCommitsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "repo", null));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                client.GetAll("owner", "name");

                connection.Received()
                    .GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/commits"),
                    Arg.Any<Dictionary<string, string>>());
            }
        }
    }
}
