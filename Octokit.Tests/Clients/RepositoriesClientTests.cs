using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

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

                connection.Received().Post<Repository>(Arg.Is<Uri>(u => u.ToString() == "user/repos"),
                    Arg.Any<NewRepository>());
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

        public class TheGenerateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Generate(null, null, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Generate("asd", null, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Generate("asd", "asd", null));
            }

            [Fact]
            public void UsesTheUserReposUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Generate("asd", "asd", new NewRepositoryFromTemplate("aName"));

                connection.Received().Post<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/asd/asd/generate"),
                    Arg.Any<NewRepositoryFromTemplate>());
            }

            [Fact]
            public void TheNewRepositoryDescription()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepository = new NewRepositoryFromTemplate("aName");

                client.Generate("anOwner", "aRepo", newRepository);

                connection.Received().Post<Repository>(Args.Uri, newRepository);
            }
        }

        public class TheTransferMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var transfer = new RepositoryTransfer("newOwner");

                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => client.Transfer(null, "name", transfer));
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => client.Transfer("owner", null, transfer));
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => client.Transfer("owner", "name", null));
            }

            [Fact]
            public async Task EnsuresNonNullArgumentsById()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var transfer = new RepositoryTransfer("newOwner");
                var repositoryId = 1;

                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => client.Transfer(repositoryId, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var transfer = new RepositoryTransfer("newOwner");

                await Assert.ThrowsAsync<ArgumentException>(
                    () => client.Transfer("", "name", transfer));
                await Assert.ThrowsAsync<ArgumentException>(
                    () => client.Transfer("owner", "", transfer));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var teamId = new long[2] { 35, 42 };
                var transfer = new RepositoryTransfer("newOwner", teamId);

                await client.Transfer("owner", "name", transfer);

                connection.Received()
                    .Post<Repository>(
                        Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/transfer"),
                        Arg.Any<RepositoryTransfer>());
            }

            [Fact]
            public async Task RequestsCorrectUrlById()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var teamId = new long[2] { 35, 42 };
                var transfer = new RepositoryTransfer("newOwner", teamId);
                var repositoryId = 1;

                await client.Transfer(repositoryId, transfer);

                connection.Received()
                    .Post<Repository>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/transfer"),
                        Arg.Any<RepositoryTransfer>());
            }

            [Fact]
            public async Task SendsCorrectRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var teamId = new long[2] { 35, 42 };
                var transfer = new RepositoryTransfer("newOwner", teamId);

                await client.Transfer("owner", "name", transfer);

                connection.Received()
                    .Post<Repository>(
                        Arg.Any<Uri>(),
                        Arg.Is<RepositoryTransfer>(t => t.NewOwner == "newOwner" && object.Equals(teamId, t.TeamIds)));
            }

            [Fact]
            public async Task SendsCorrectRequestById()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var teamId = new long[2] { 35, 42 };
                var transfer = new RepositoryTransfer("newOwner", teamId);
                var repositoryId = 1;

                await client.Transfer(repositoryId, transfer);

                connection.Received()
                    .Post<Repository>(
                        Arg.Any<Uri>(),
                        Arg.Is<RepositoryTransfer>(t => t.NewOwner == "newOwner" && object.Equals(teamId, t.TeamIds)));
            }

            [Fact]
            public async Task SendsPreviewHeader()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var teamId = new long[2] { 35, 42 };
                var transfer = new RepositoryTransfer("newOwner", teamId);

                await client.Transfer("owner", "name", transfer);

                connection.Received()
                    .Post<Repository>(
                        Arg.Any<Uri>(),
                        Arg.Any<RepositoryTransfer>());
            }

            [Fact]
            public async Task SendsPreviewHeaderById()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var teamId = new long[2] { 35, 42 };
                var transfer = new RepositoryTransfer("newOwner", teamId);
                var repositoryId = 1;

                await client.Transfer(repositoryId, transfer);

                connection.Received()
                    .Post<Repository>(
                        Arg.Any<Uri>(),
                        Arg.Any<RepositoryTransfer>())
                        ;
            }
        }

        public class TheAreVulnerabilityAlertsEnabledMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = CreateResponse(status);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/vulnerability-alerts"),
                    null, null).Returns(responseTask);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new RepositoriesClient(apiConnection);

                var result = await client.AreVulnerabilityAlertsEnabled("owner", "name");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = CreateResponse(HttpStatusCode.Conflict);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/vulnerability-alerts"),
                    null, null).Returns(responseTask);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new RepositoriesClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.AreVulnerabilityAlertsEnabled("owner", "name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AreVulnerabilityAlertsEnabled(null, "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AreVulnerabilityAlertsEnabled("", "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AreVulnerabilityAlertsEnabled( "owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AreVulnerabilityAlertsEnabled("owner", ""));
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

                connection.Received()
                    .Get<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name"),
                    null);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.Get(1);

                connection.Received()
                    .Get<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories/1"));
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
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "user/repos"),
                    null,
                    Args.ApiOptions);
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
                        Arg.Is<Dictionary<string, string>>(d => d["type"] == "private" && d["sort"] == "full_name"),
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
                        Arg.Is<Dictionary<string, string>>(d => d["type"] == "member" && d["sort"] == "updated" && d["direction"] == "asc"),
                        Args.ApiOptions);
            }

            [Fact]
            public async Task CanFilterByVisibility()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                var request = new RepositoryRequest
                {
                    Visibility = RepositoryRequestVisibility.Private
                };

                await client.GetAllForCurrent(request);

                connection.Received()
                    .GetAll<Repository>(
                        Arg.Is<Uri>(u => u.ToString() == "user/repos"),
                        Arg.Is<Dictionary<string, string>>(d => d["visibility"] == "private"),
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
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "owner" && d["sort"] == "full_name"),
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
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgname/repos"), null, Args.ApiOptions);
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

        public class TheGetLicenseContentsMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetLicenseContents("owner", "name");

                connection.Received()
                    .Get<RepositoryContentLicense>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/license"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                await client.GetLicenseContents(1);

                connection.Received()
                    .Get<RepositoryContentLicense>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/license"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetLicenseContents(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetLicenseContents("owner", null));
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

        public class TheEditMethod
        {
            [Fact]
            public void PatchesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var update = new RepositoryUpdate() { Name= "repo" };

                client.Edit("owner", "repo", update);

                connection.Received()
                    .Patch<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo"),
                    Arg.Any<RepositoryUpdate>());
            }

            [Fact]
            public void PatchesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var update = new RepositoryUpdate() { Name= "repo" };

                client.Edit(1, update);

                connection.Received()
                    .Patch<Repository>(Arg.Is<Uri>(u => u.ToString() == "repositories/1"), Arg.Any<RepositoryUpdate>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());
                var update = new RepositoryUpdate() { Name= "anyreponame" };

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


                var options = new ApiOptions();
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare(null, "repo", "base", "head", options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("", "repo", "base", "head", options));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", null, "base", "head", options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "", "base", "head", options));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", "repo", null, "head", options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "repo", "", "head", options));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", "repo", "base", null, options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Compare("owner", "repo", "base", "", options));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", "repo", "base", "head", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Compare("owner", "repo", "base", "head", null));
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

        public class TheGetSha1Method
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryCommitsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("", "name", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "name", ""));
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
                    .Get<string>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/commits/reference"), null);
            }
        }

        public class TheGetAllTopicsMethod
        {
            readonly RepositoriesClient _client = new RepositoriesClient(Substitute.For<IApiConnection>());

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                await Assert.ThrowsAsync<ArgumentNullException>(() => _client.GetAllTopics(123, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => _client.GetAllTopics("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => _client.GetAllTopics(null, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => _client.GetAllTopics("owner", null, ApiOptions.None));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                await Assert.ThrowsAsync<ArgumentException>(() => _client.GetAllTopics(string.Empty, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => _client.GetAllTopics("owner", string.Empty, ApiOptions.None));
            }

            [Fact]
            public void RequestsTheCorrectUrlForOwnerAndRepo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllTopics("owner", "name");

                connection.Received()
                    .Get<RepositoryTopics>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/topics"), null);
            }

            [Fact]
            public void RequestsTheCorrectUrlForRepoId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.GetAllTopics(1234);

                connection.Received()
                    .Get<RepositoryTopics>(Arg.Is<Uri>(u => u.ToString() == "repositories/1234/topics"), null);
            }
        }

        public class TheReplaceAllTopicsMethod
        {
            private readonly RepositoryTopics _emptyTopics = new RepositoryTopics();
            private readonly RepositoryTopics _listOfTopics = new RepositoryTopics(new List<string> { "one", "two", "three" });
            private readonly IApiConnection _connection;
            private readonly RepositoriesClient _client;

            public TheReplaceAllTopicsMethod()
            {
                _connection = Substitute.For<IApiConnection>();
                _client = new RepositoriesClient(_connection);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                await Assert.ThrowsAsync<ArgumentNullException>(() => _client.ReplaceAllTopics("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => _client.ReplaceAllTopics(123, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => _client.ReplaceAllTopics(null, "repo", _emptyTopics));
                await Assert.ThrowsAsync<ArgumentNullException>(() => _client.ReplaceAllTopics("owner", null, _emptyTopics));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                await Assert.ThrowsAsync<ArgumentException>(() => _client.ReplaceAllTopics(string.Empty, "repo", _emptyTopics));
                await Assert.ThrowsAsync<ArgumentException>(() => _client.ReplaceAllTopics("owner", string.Empty, _emptyTopics));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlForOwnerAndRepoWithEmptyTopics()
            {
                await _client.ReplaceAllTopics("owner", "name", _emptyTopics);

                _connection.Received()
                    .Put<RepositoryTopics>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/topics"), _emptyTopics);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlForOwnerAndRepoWithListOfTopics()
            {
                await _client.ReplaceAllTopics("owner", "name", _listOfTopics);

                _connection.Received()
                    .Put<RepositoryTopics>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/topics"), _listOfTopics);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlForRepoIdWithEmptyTopics()
            {
                await _client.ReplaceAllTopics(1234, _emptyTopics);

                _connection.Received()
                    .Put<RepositoryTopics>(Arg.Is<Uri>(u => u.ToString() == "repositories/1234/topics"), _emptyTopics);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlForRepoIdWithListOfTopics()
            {
                await _client.ReplaceAllTopics(1234, _listOfTopics);

                _connection.Received()
                    .Put<RepositoryTopics>(Arg.Is<Uri>(u => u.ToString() == "repositories/1234/topics"), _listOfTopics);
            }
        }
    }
}
