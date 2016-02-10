using Octokit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            using (var context = await client.CreateRepositoryContext("public-repo"))
            {
                var branchFromMaster = await fixture.CreateBranch(context.RepositoryOwner, context.RepositoryName, "patch-1");

                var branchFromPath = await fixture.CreateBranch(context.RepositoryOwner, context.RepositoryName, "patch-2", branchFromMaster);

                var allBrancheNames = (await client.Repository.GetAllBranches(context.RepositoryOwner, context.RepositoryName)).Select(b => b.Name);

                Assert.Contains("patch-1", allBrancheNames);
                Assert.Contains("patch-2", allBrancheNames);
            }
        }
    }
}
