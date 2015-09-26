using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class BranchesClientTests
{
    public class TheGetBranchesMethod
    {
        public TheGetBranchesMethod() { }

        [IntegrationTest]
        public async Task ReturnsBranches()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                var branches = await github.Repository.GetAllBranches(context.Repository.Owner.Login, context.Repository.Name);

                Assert.NotEmpty(branches);
                Assert.Equal(branches[0].Name, "master");
            }
        }
    }
}