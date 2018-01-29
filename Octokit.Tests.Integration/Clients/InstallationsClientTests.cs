using Octokit.Tests.Integration.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class InstallationsClientTests
    {
        public class TheGetAllMethod : IDisposable
        {
            IGitHubClient _github;
            RepositoryContext _context;

            public TheGetAllMethod()
            {
                _github = Helper.GetAuthenticatedClient();
                var repoName = Helper.MakeNameWithTimestamp("public-repo");

                _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
            }

            [IntegrationTest]
            public async Task GetsAllInstallations()
            {
                var result = await _github.Installation.GetAll();

                // TODO - find a way to run this test, and see the results.

                //Assert.Equal(2, result.Count);
                //Assert.True(result.FirstOrDefault(x => x.Id == card1.Id).Id == card1.Id);
                //Assert.True(result.FirstOrDefault(x => x.Id == card2.Id).Id == card2.Id);
            }

            public void Dispose()
            {
                if (_context != null)
                    _context.Dispose();
            }
        }
    }
}
