using System;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octokit.Clients;
using Octokit.Http;
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

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Repository>>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.Get("fake", "repo");

                client.Received().Get(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var repositoriesClient = new RepositoriesClient(Substitute.For<IApiConnection<Repository>>());

                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.Get(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.Get("owner", null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var client = Substitute.For<IApiConnection<Repository>>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.GetAllForCurrent();

                client.Received()
                    .GetAll(Arg.Is<Uri>(u => u.ToString() == "user/repos"));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var client = Substitute.For<IApiConnection<Repository>>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.GetAllForUser("username");

                client.Received()
                    .GetAll(Arg.Is<Uri>(u => u.ToString() == "/users/username/repos"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection<Repository>>());

                AssertEx.Throws<ArgumentNullException>(async () => await reposEndpoint.GetAllForUser(null));
            }
        }

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var client = Substitute.For<IApiConnection<Repository>>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.GetAllForOrg("orgname");

                client.Received()
                    .GetAll(Arg.Is<Uri>(u => u.ToString() == "/orgs/orgname/repos"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection<Repository>>());

                AssertEx.Throws<ArgumentNullException>(async () => await reposEndpoint.GetAllForOrg(null));
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
                var client = Substitute.For<IApiConnection<Repository>>();
                client.GetItem<ReadmeResponse>(Args.Uri).Returns(Task.FromResult(readmeInfo));
                client.GetHtml(Args.Uri).Returns(Task.FromResult("<html>README</html>"));
                var reposEndpoint = new RepositoriesClient(client);

                var readme = await reposEndpoint.GetReadme("fake", "repo");

                readme.Name.Should().Be("README.md");
                client.Received().GetItem<ReadmeResponse>(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/readme"));
                client.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"));
                var htmlReadme = await readme.GetHtmlContent();
                htmlReadme.Should().Be("<html>README</html>");
                client.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"));
            }
        }
    }
}
