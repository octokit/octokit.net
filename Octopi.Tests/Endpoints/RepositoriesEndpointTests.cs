using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octopi.Endpoints;
using Octopi.Http;
using Octopi.Tests.Helpers;
using Xunit;

namespace Octopi.Tests
{
    public class RepositoriesEndpointTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoriesEndpoint(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task ReturnsSpecifiedRepository()
            {
                Uri endpoint = null;
                var returnedRepo = new Repository();
                var response = Task.FromResult<IResponse<Repository>>(new ApiResponse<Repository>
                {
                    BodyAsObject = returnedRepo
                });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<Repository>(Args.Uri)
                    .Returns(ctx =>
                    {
                        endpoint = ctx.Arg<Uri>();
                        return response;
                    });

                var client = new RepositoriesEndpoint(connection);

                var repo = await client.Get("owner", "repo");

                repo.Should().NotBeNull();
                repo.Should().BeSameAs(returnedRepo);
                endpoint.Should().Be(new Uri("/repos/owner/repo", UriKind.Relative));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesEndpoint(Substitute.For<IConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await reposEndpoint.Get(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await reposEndpoint.Get("owner", null));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesEndpoint(Substitute.For<IConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await reposEndpoint.GetAllForUser(null));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var links = new Dictionary<string, Uri>();
                var scopes = new List<string>();
                IResponse<List<Repository>> response = new ApiResponse<List<Repository>>
                {
                    ApiInfo = new ApiInfo(links, scopes, scopes, "", 1, 1),
                    BodyAsObject = new List<Repository>
                    {
                        new Repository { Name = "One" },
                        new Repository { Name = "Two" }
                    }
                };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<List<Repository>>(Args.Uri).Returns(Task.FromResult(response));
                var reposEndpoint = new RepositoriesEndpoint(connection);

                var repositories = await reposEndpoint.GetAllForUser("username");

                repositories.Count.Should().Be(2);
                connection.Received()
                    .GetAsync<List<Repository>>(Arg.Is<Uri>(u => u.ToString() == "/users/username/repos"));
            }
        }

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesEndpoint(Substitute.For<IConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await reposEndpoint.GetAllForOrg(null));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var links = new Dictionary<string, Uri>();
                var scopes = new List<string>();
                IResponse<List<Repository>> response = new ApiResponse<List<Repository>>
                {
                    ApiInfo = new ApiInfo(links, scopes, scopes, "", 1, 1),
                    BodyAsObject = new List<Repository>
                    {
                        new Repository { Name = "One" },
                        new Repository { Name = "Two" }
                    }
                };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<List<Repository>>(Args.Uri).Returns(Task.FromResult(response));
                var reposEndpoint = new RepositoriesEndpoint(connection);

                var repositories = await reposEndpoint.GetAllForOrg("orgName");

                repositories.Count.Should().Be(2);
                connection.Received()
                    .GetAsync<List<Repository>>(Arg.Is<Uri>(u => u.ToString() == "/orgs/orgName/repos"));
            }
        }

        public class TheGetReadmeMethod
        {
            [Fact]
            public async Task ReturnsReadme()
            {
                var links = new Dictionary<string, Uri>();
                var scopes = new List<string>();
                string encodedContent = Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello world"));
                IResponse<ReadmeResponse> apiResponse = new ApiResponse<ReadmeResponse>
                {
                    ApiInfo = new ApiInfo(links, scopes, scopes, "", 1, 1),
                    BodyAsObject = new ReadmeResponse
                    {
                        Content = encodedContent,
                        Encoding = "base64",
                        Name = "README.md",
                        Url = "https://github.example.com/readme.md",
                        HtmlUrl = "https://github.example.com/readme"
                    }
                };
                IResponse<string> htmlResponse = new ApiResponse<string>
                {
                    Body = "<html></html>"
                };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<ReadmeResponse>(Args.Uri).Returns(Task.FromResult(apiResponse));
                connection.GetHtml(Args.Uri).Returns(Task.FromResult(htmlResponse));
                var reposEndpoint = new RepositoriesEndpoint(connection);

                var readme = await reposEndpoint.GetReadme("fake", "repo");
                readme.Name.Should().Be("README.md");
                connection.Received()
                    .GetAsync<ReadmeResponse>(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/readme"));
                connection.DidNotReceive()
                    .GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"));
                var htmlReadme = await readme.GetHtmlContent();
                htmlReadme.Should().Be("<html></html>");
                connection.Received()
                    .GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"));

            }
        }
    }
}
