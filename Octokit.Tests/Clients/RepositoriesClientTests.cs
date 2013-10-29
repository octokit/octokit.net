using System;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
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
                var repositoriesClient = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.Create(null));
                await AssertEx.Throws<ArgumentException>(async () => await repositoriesClient.Create(new NewRepository { Name = null }));
            }
            
            [Fact]
            public void UsesTheUserReposUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.Create(new NewRepository { Name = "aName" });

                client.Received().Post<Repository>(Arg.Is<Uri>(u => u.ToString() == "user/repos"), Arg.Any<NewRepository>());
            }

            [Fact]
            public void TheNewRepositoryDescription()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);
                var newRepository = new NewRepository { Name = "aName" };

                repositoriesClient.Create(newRepository);

                client.Received().Post<Repository>(Arg.Any<Uri>(), newRepository);
            }
        }

        public class TheCreateMethodForOrganization
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var repositoriesClient = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.Create(null, new NewRepository { Name = "aName" }));
                await AssertEx.Throws<ArgumentException>(async () => await repositoriesClient.Create("aLogin", null));
                await AssertEx.Throws<ArgumentException>(async () => await repositoriesClient.Create("aLogin", new NewRepository { Name = null }));
            }

            [Fact]
            public async Task UsesTheOrganizatinosReposUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);

                await repositoriesClient.Create("theLogin", new NewRepository { Name = "aName" });

                client.Received().Post<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/theLogin/repos"),
                    Args.NewRepository);
            }

            [Fact]
            public async Task TheNewRepositoryDescription()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);
                var newRepository = new NewRepository { Name = "aName" };

                await repositoriesClient.Create("aLogin", newRepository);

                client.Received().Post<Repository>(Arg.Any<Uri>(), newRepository);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var repositoriesClient = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.Delete(null, "aRepoName"));
                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.Delete("anOwner", null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);

                await repositoriesClient.Delete("theOwner", "theRepoName");

                client.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/theOwner/theRepoName"));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.Get("fake", "repo");

                client.Received().Get<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var repositoriesClient = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.Get(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await repositoriesClient.Get("owner", null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.GetAllForCurrent();

                client.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "user/repos"));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.GetAllForUser("username");

                client.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "users/username/repos"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await reposEndpoint.GetAllForUser(null));
            }
        }

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlAndReturnsOrganizations()
            {
                var client = Substitute.For<IApiConnection>();
                var repositoriesClient = new RepositoriesClient(client);

                repositoriesClient.GetAllForOrg("orgname");

                client.Received()
                    .GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgname/repos"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection>());

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
                var client = Substitute.For<IApiConnection>();
                client.Get<ReadmeResponse>(Args.Uri, null).Returns(Task.FromResult(readmeInfo));
                client.GetHtml(Args.Uri, null).Returns(Task.FromResult("<html>README</html>"));
                var reposEndpoint = new RepositoriesClient(client);

                var readme = await reposEndpoint.GetReadme("fake", "repo");

                Assert.Equal("README.md", readme.Name);
                client.Received().Get<ReadmeResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/readme"), 
                    null);
                client.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"), 
                    null);
                var htmlReadme = await readme.GetHtmlContent();
                Assert.Equal("<html>README</html>", htmlReadme);
                client.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"), null);
            }
        }

        public class TheGetReadmeHtmlMethod
        {
            [Fact]
            public async Task ReturnsReadmeHtml()
            {
                var client = Substitute.For<IApiConnection>();
                client.GetHtml(Args.Uri, null).Returns(Task.FromResult("<html>README</html>"));
                var reposEndpoint = new RepositoriesClient(client);

                var readme = await reposEndpoint.GetReadmeHtml("fake", "repo");

                client.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/readme"), null);
                Assert.Equal("<html>README</html>", readme);
            }
        }
    }
}
