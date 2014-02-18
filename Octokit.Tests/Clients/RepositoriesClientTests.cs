using System;
using System.Collections.Generic;
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

                connection.Received().Post<Repository>(Arg.Any<Uri>(), newRepository);
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

                connection.Received().Post<Repository>(Arg.Any<Uri>(), newRepository);
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
            public void EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => reposEndpoint.GetAllForUser(null));
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
            public void EnsuresNonNullArguments()
            {
                var reposEndpoint = new RepositoriesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => reposEndpoint.GetAllForOrg(null));
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
                connection.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"),
                    null);
                var htmlReadme = await readme.GetHtmlContent();
                Assert.Equal("<html>README</html>", htmlReadme);
                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"), null);
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
            public void EnsuresArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllBranches(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllBranches("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllBranches("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllBranches("owner", ""));
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
                    .GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/contributors"), Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public void EnsuresArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("owner", ""));
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
            public void EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllLanguages(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllLanguages("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllLanguages("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllLanguages("owner", ""));
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
            public void EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllTeams("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllTeams("owner", ""));
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
            public void EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllTags(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTags("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllTags("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllTags("owner", ""));
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
            public void EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetBranch(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetBranch("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetBranch("owner", "repo", null));
                Assert.Throws<ArgumentException>(() => client.GetBranch("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetBranch("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetBranch("owner", "repo", ""));
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
            public void EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());
                var update = new RepositoryUpdate();

                Assert.Throws<ArgumentNullException>(() => client.Edit(null, "repo", update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", null, update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", "repo", null));
                Assert.Throws<ArgumentException>(() => client.Edit("", "repo", update));
                Assert.Throws<ArgumentException>(() => client.Edit("owner", "", update));
            }
        }
    }
}
