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
    public class RepositoriesClientTests
    {
        public class TheCtor
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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null));
            }

            [Fact]
            public void UsesTheUserReposUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Create(new NewRepository("aName"));

                connection.Received().Post<Repository>(Arg.Is<Uri>(u => u.ToString() == "user/repos"), Arg.Any<NewRepository>());
            }

            [Fact]
            public void TheNewRepositoryDescription()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepository = new NewRepository("aName");

                client.Create(newRepository);

                connection.Received().Post<Repository>(Args.Uri, newRepository);
            }

            [Fact]
            public async Task ThrowsRepositoryExistsExceptionWhenRepositoryExistsForCurrentUser()
            {
                var newRepository = new NewRepository("aName");
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

                var exception = await Assert.ThrowsAsync<RepositoryExistsException>(
                    () => client.Create(newRepository));

                Assert.False(exception.OwnerIsOrganization);
                Assert.Null(exception.Organization);
                Assert.Equal("aName", exception.RepositoryName);
                Assert.Null(exception.ExistingRepositoryWebUrl);
            }

            [Fact]
            public async Task ThrowsExceptionWhenPrivateRepositoryQuotaExceeded()
            {
                var newRepository = new NewRepository("aName") { Private = true };
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

                var exception = await Assert.ThrowsAsync<PrivateRepositoryQuotaExceededException>(
                    () => client.Create(newRepository));

                Assert.NotNull(exception);
            }
        }

        public class TheCreateMethodForOrganization
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, new NewRepository("aName")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("aLogin", null));
            }

            [Fact]
            public async Task UsesTheOrganizationsReposUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.Create("theLogin", new NewRepository("aName"));

                connection.Received().Post<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/theLogin/repos"),
                    Args.NewRepository);
            }

            [Fact]
            public async Task TheNewRepositoryDescription()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepository = new NewRepository("aName");

                await client.Create("aLogin", newRepository);

                connection.Received().Post<Repository>(Args.Uri, newRepository);
            }

            [Fact]
            public async Task ThrowsRepositoryExistsExceptionWhenRepositoryExistsForSpecifiedOrg()
            {
                var newRepository = new NewRepository("aName");
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

                var exception = await Assert.ThrowsAsync<RepositoryExistsException>(
                    () => client.Create("illuminati", newRepository));

                Assert.True(exception.OwnerIsOrganization);
                Assert.Equal("illuminati", exception.Organization);
                Assert.Equal("aName", exception.RepositoryName);
                Assert.Equal(new Uri("https://github.com/illuminati/aName"), exception.ExistingRepositoryWebUrl);
                Assert.Equal("There is already a repository named 'aName' in the organization 'illuminati'.",
                    exception.Message);
            }

            [Fact]
            public async Task ThrowsValidationException()
            {
                var newRepository = new NewRepository("aName");
                var response = Substitute.For<IResponse>();
                response.StatusCode.Returns((HttpStatusCode)422);
                response.Body.Returns(@"{""message"":""Validation Failed"",""documentation_url"":"
                    + @"""http://developer.github.com/v3/repos/#create"",""errors"":[]}");
                var connection = Substitute.For<IApiConnection>();
                connection.Connection.BaseAddress.Returns(GitHubClient.GitHubApiUrl);
                connection.Post<Repository>(Args.Uri, newRepository)
                    .Returns<Task<Repository>>(_ => { throw new ApiValidationException(response); });
                var client = new RepositoriesClient(connection);

                var exception = await Assert.ThrowsAsync<ApiValidationException>(
                    () => client.Create("illuminati", newRepository));

                Assert.Null(exception as RepositoryExistsException);
            }

            [Fact]
            public async Task ThrowsRepositoryExistsExceptionForEnterpriseInstance()
            {
                var newRepository = new NewRepository("aName");
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

                var exception = await Assert.ThrowsAsync<RepositoryExistsException>(
                    () => client.Create("illuminati", newRepository));

                Assert.Equal("aName", exception.RepositoryName);
                Assert.Equal(new Uri("https://example.com/illuminati/aName"), exception.ExistingRepositoryWebUrl);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.Delete("owner", "name");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.Delete(1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", ""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.Get("owner", "name");

                connection.Received().Get<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/name"),
                    null,
                    "application/vnd.github.polaris-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.Get(1);

                connection.Received().Get<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1"),
                    null,
                    "application/vnd.github.polaris-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", ""));
            }
        }

        public class TheGetAllPublicMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrlAndReturnsRepositories()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllPublic();

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories"));
            }
        }

        public class TheGetAllPublicSinceMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllPublic(new PublicRepositoryRequest(364L));

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories?since=364"));
            }

            [Fact]
            public async Task SendsTheCorrectParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllPublic(new PublicRepositoryRequest(364L));

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories?since=364"));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrlAndReturnsRepositories()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllForCurrent();

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "user/repos"), Args.ApiOptions);
            }

            [Fact]
            public async Task CanFilterByType()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var request = new RepositoryRequest
                {
                    Type = RepositoryType.All
                };

                await client.GetAllForCurrent(request);

                connection.Received()
                    .GetAll<Repository>(
                        Arg.Is<Uri>(u => u.ToString() == "user/repos"),
                        Arg.Is<Dictionary<string, string>>(d => d["type"] == "all"),
                        Args.ApiOptions);
            }

            [Fact]
            public async Task CanFilterBySort()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var request = new RepositoryRequest
                {
                    Type = RepositoryType.Private,
                    Sort = RepositorySort.FullName
                };

                await client.GetAllForCurrent(request);

                connection.Received()
                    .GetAll<Repository>(
                        Arg.Is<Uri>(u => u.ToString() == "user/repos"),
                        Arg.Is<Dictionary<string, string>>(d =>
                            d["type"] == "private" && d["sort"] == "full_name"),
                        Args.ApiOptions);
            }

            [Fact]
            public async Task CanFilterBySortDirection()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var request = new RepositoryRequest
                {
                    Type = RepositoryType.Member,
                    Sort = RepositorySort.Updated,
                    Direction = SortDirection.Ascending
                };

                await client.GetAllForCurrent(request);

                connection.Received()
                    .GetAll<Repository>(
                        Arg.Is<Uri>(u => u.ToString() == "user/repos"),
                        Arg.Is<Dictionary<string, string>>(d =>
                            d["type"] == "member" && d["sort"] == "updated" && d["direction"] == "asc"),
                        Args.ApiOptions);
            }

            [Fact]
            public async Task CanFilterByVisibility()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var request = new RepositoryRequest
                {
                    Visibility = RepositoryVisibility.Private
                };

                await client.GetAllForCurrent(request);

                connection.Received()
                    .GetAll<Repository>(
                        Arg.Is<Uri>(u => u.ToString() == "user/repos"),
                        Arg.Is<Dictionary<string, string>>(d =>
                            d["visibility"] == "private"),
                        Args.ApiOptions);
            }

            [Fact]
            public async Task CanFilterByAffiliation()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var request = new RepositoryRequest
                {
                    Affiliation = RepositoryAffiliation.Owner,
                    Sort = RepositorySort.FullName
                };

                await client.GetAllForCurrent(request);

                connection.Received()
                    .GetAll<Repository>(
                        Arg.Is<Uri>(u => u.ToString() == "user/repos"),
                        Arg.Is<Dictionary<string, string>>(d =>
                            d["affiliation"] == "owner" && d["sort"] == "full_name"),
                        Args.ApiOptions);
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrlAndReturnsRepositories()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllForUser("username");

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "users/username/repos"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => reposEndpoint.GetAllForUser(null));
                await Assert.ThrowsAsync<ArgumentException>(() => reposEndpoint.GetAllForUser(""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => reposEndpoint.GetAllForUser(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => reposEndpoint.GetAllForUser("user", null));

                await Assert.ThrowsAsync<ArgumentException>(() => reposEndpoint.GetAllForUser("", ApiOptions.None));
            }
        }

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllForOrg("orgname");

                connection.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgname/repos"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => reposEndpoint.GetAllForOrg(null));
                await Assert.ThrowsAsync<ArgumentException>(() => reposEndpoint.GetAllForOrg(""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => reposEndpoint.GetAllForOrg(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => reposEndpoint.GetAllForOrg("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => reposEndpoint.GetAllForOrg("", ApiOptions.None));
            }
        }

        public class TheGetAllBranchesMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllBranches("owner", "name");

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/branches"), null, "application/vnd.github.loki-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllBranches(1);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches"), null, "application/vnd.github.loki-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllBranches("owner", "name", options);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/branches"), null, "application/vnd.github.loki-preview+json", options);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllBranches(1, options);

                connection.Received()
                    .GetAll<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches"), null, "application/vnd.github.loki-preview+json", options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllBranches(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllBranches("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllBranches(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllBranches("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllBranches("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllBranches(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllBranches("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllBranches("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllBranches("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllBranches("owner", "", ApiOptions.None));
            }
        }

        public class TheGetAllContributorsMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllContributors("owner", "name");

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/contributors"), Arg.Any<IDictionary<string, string>>(), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllContributors(1);

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contributors"), Arg.Any<IDictionary<string, string>>(), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllContributors("owner", "name", options);

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/contributors"), Arg.Any<IDictionary<string, string>>(), options);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllContributors(1, options);

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contributors"), Arg.Any<IDictionary<string, string>>(), options);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlIncludeAnonymous()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllContributors("owner", "name", true);

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/contributors"), Arg.Is<IDictionary<string, string>>(d => d["anon"] == "1"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdIncludeAnonymous()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllContributors(1, true);

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contributors"), Arg.Is<IDictionary<string, string>>(d => d["anon"] == "1"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptionsIncludeAnonymous()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllContributors("owner", "name", true, options);

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/contributors"), Arg.Is<IDictionary<string, string>>(d => d["anon"] == "1"), options);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdWithApiOptionsIncludeAnonymous()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllContributors(1, true, options);

                connection.Received()
                    .GetAll<RepositoryContributor>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contributors"), Arg.Is<IDictionary<string, string>>(d => d["anon"] == "1"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors(null, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors(null, "repo", false, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors("owner", null, false, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors("owner", "repo", false, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors(1, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContributors(1, false, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContributors("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContributors("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContributors("", "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContributors("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContributors("", "repo", false, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContributors("owner", "", false, ApiOptions.None));
            }
        }

        public class TheGetAllLanguagesMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllLanguages("owner", "name");

                connection.Received()
                    .Get<Dictionary<string, long>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/languages"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllLanguages(1);

                connection.Received()
                    .Get<Dictionary<string, long>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/languages"));
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
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllTeams("owner", "name");

                connection.Received()
                    .GetAll<Team>(
                        Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/teams"),
                        Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllTeams(1);

                connection.Received()
                    .GetAll<Team>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/teams"),
                        Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllTeams("owner", "name", options);

                connection.Received()
                    .GetAll<Team>(
                        Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/teams"),
                        options);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllTeams(1, options);

                connection.Received()
                    .GetAll<Team>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/teams"),
                        options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTeams(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTeams("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTeams(null, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTeams("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTeams("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTeams(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTeams("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTeams("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTeams("", "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTeams("owner", "", ApiOptions.None));
            }
        }

        public class TheGetAllTagsMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllTags("owner", "name");

                connection.Received()
                    .GetAll<RepositoryTag>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/tags"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetAllTags(1);

                connection.Received()
                    .GetAll<RepositoryTag>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/tags"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllTags("owner", "name", options);

                connection.Received()
                    .GetAll<RepositoryTag>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/tags"), options);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllTags(1, options);

                connection.Received()
                    .GetAll<RepositoryTag>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/tags"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTags(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTags("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTags(null, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTags("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTags("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllTags(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTags("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTags("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTags("", "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllTags("owner", "", ApiOptions.None));
            }
        }

        public class TheGetBranchMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetBranch("owner", "repo", "branch");

                connection.Received()
                    .Get<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch"), null, "application/vnd.github.loki-preview+json");
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetBranch(1, "branch");

                connection.Received()
                    .Get<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch"), null, "application/vnd.github.loki-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranch(null, "repo", "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranch("owner", null, "branch"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranch("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetBranch(1, null));

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
                var update = new RepositoryUpdate("repo");

                client.Edit("owner", "repo", update);

                connection.Received()
                    .Patch<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo"), Arg.Any<RepositoryUpdate>(), "application/vnd.github.polaris-preview+json");
            }

            [Fact]
            public void PatchesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var update = new RepositoryUpdate("repo");

                client.Edit(1, update);

                connection.Received()
                    .Patch<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories/1"), Arg.Any<RepositoryUpdate>(), "application/vnd.github.polaris-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());
                var update = new RepositoryUpdate();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(null, "repo", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(1, null));

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
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();

                var client = new RepositoryCommitsClient(connection);

                client.Compare("owner", "repo", "base", "head");

                connection.Received()
                    .Get<CompareResult>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/compare/base...head"));
            }

            [Fact]
            public void EncodesUrl()
            {
                var connection = Substitute.For<IApiConnection>();

                var client = new RepositoryCommitsClient(connection);

                client.Compare("owner", "repo", "base", "shiftkey/my-cool-branch");

                connection.Received()
                    .Get<CompareResult>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/compare/base...shiftkey%2Fmy-cool-branch"));
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
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                client.Get("owner", "name", "reference");

                connection.Received()
                    .Get<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/commits/reference"));
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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "repo", null, ApiOptions.None));
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                client.GetAll("owner", "name");

                connection.Received()
                    .GetAll<GitHubCommit>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/commits"), Args.EmptyDictionary, Args.ApiOptions);
            }
        }

        public class TheEditBranchMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var update = new BranchUpdate();
                const string previewAcceptsHeader = "application/vnd.github.loki-preview+json";

                client.EditBranch("owner", "repo", "branch", update);

                connection.Received()
                    .Patch<Branch>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/branches/branch"), Arg.Any<BranchUpdate>(), previewAcceptsHeader);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var update = new BranchUpdate();
                const string previewAcceptsHeader = "application/vnd.github.loki-preview+json";

                client.EditBranch(1, "branch", update);

                connection.Received()
                    .Patch<Branch>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/branches/branch"), Arg.Any<BranchUpdate>(), previewAcceptsHeader);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());
                var update = new BranchUpdate();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditBranch(null, "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditBranch("owner", null, "branch", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditBranch("owner", "repo", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditBranch("owner", "repo", "branch", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditBranch(1, null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditBranch(1, "branch", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.EditBranch("", "repo", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.EditBranch("owner", "", "branch", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.EditBranch("owner", "repo", "", update));

                await Assert.ThrowsAsync<ArgumentException>(() => client.EditBranch(1, "", update));
            }
        }

        public class TheGetSha1Method
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new RepositoryCommitsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("", "name", "reference"));
                Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "", "reference"));
                Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "name", ""));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new RepositoryCommitsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1(null, "name", "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1("owner", null, "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1("owner", "name", null));
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCommitsClient(connection);

                client.GetSha1("owner", "name", "reference");

                connection.Received()
                    .Get<string>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/commits/reference"), null, AcceptHeaders.CommitReferenceSha1Preview);
            }
        }
    }
}
