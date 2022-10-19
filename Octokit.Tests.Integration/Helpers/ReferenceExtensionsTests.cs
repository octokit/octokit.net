using Octokit.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Helpers
{
    public class ReferenceExtensionsTests
    {
        [IntegrationTest]
        public async Task CreateABranch()
        {
            var client = Helper.GetAuthenticatedClient();
            var fixture = client.Git.Reference;

            using (var context = await client.CreateRepositoryContextWithAutoInit("public-repo"))
            {
                var branchFromMain = await fixture.CreateBranch(context.RepositoryOwner, context.RepositoryName, "patch-1", context.Repository.DefaultBranch);

                var branchFromPath = await fixture.CreateBranch(context.RepositoryOwner, context.RepositoryName, "patch-2", branchFromMain);

                var allBranchNames = (await client.Repository.Branch.GetAll(context.RepositoryOwner, context.RepositoryName)).Select(b => b.Name);

                Assert.Contains("patch-1", allBranchNames);
                Assert.Contains("patch-2", allBranchNames);
            }
        }
    }
}
