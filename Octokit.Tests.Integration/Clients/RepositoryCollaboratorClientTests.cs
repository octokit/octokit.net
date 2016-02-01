using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class RepositoryCollaboratorClientTests
{
    public class TheGetAllMethod
    {
        [IntegrationTest]
        public async Task ReturnsAllCollaborators()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add a collaborator
                await fixture.Add(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName);
                Assert.NotNull(collaborators);
                Assert.Equal(2, collaborators.Count);
            }
        }
    }

    public class TheIsCollaboratorMethod
    {
        [IntegrationTest]
        public async Task ReturnsTrueIfUserIsCollaborator()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add a collaborator
                fixture.Add(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                var isCollab = await fixture.IsCollaborator(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                Assert.True(isCollab);
            }
        }
    }
}