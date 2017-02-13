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
                Assert.NotNull(collaborators[0].Permissions);
                Assert.NotNull(collaborators[1].Permissions);
            }
        }

        [IntegrationTest]
        public async Task ReturnsAllCollaboratorsWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add a collaborator
                await fixture.Add(context.Repository.Id, "m-zuber-octokit-integration-tests");

                var collaborators = await fixture.GetAll(context.Repository.Id);
                Assert.NotNull(collaborators);
                Assert.Equal(2, collaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithoutStart()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add some collaborators
                await fixture.Add(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, options);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithoutStartAndRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add some collaborators
                await fixture.Add(context.Repository.Id, "m-zuber-octokit-integration-tests");

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var collaborators = await fixture.GetAll(context.Repository.Id, options);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithStart()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add some collaborators
                await fixture.Add(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, options);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithStartAndRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add some collaborators
                await fixture.Add(context.Repository.Id, "m-zuber-octokit-integration-tests");

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var collaborators = await fixture.GetAll(context.Repository.Id, options);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add some collaborators
                await fixture.Add(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add some collaborators
                await fixture.Add(context.Repository.Id, "m-zuber-octokit-integration-tests");

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await fixture.GetAll(context.Repository.Id, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await fixture.GetAll(context.Repository.Id, skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
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
                await fixture.Add(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                var isCollab = await fixture.IsCollaborator(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                Assert.True(isCollab);
            }
        }

        [IntegrationTest]
        public async Task ReturnsTrueIfUserIsCollaboratorWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add a collaborator
                await fixture.Add(context.Repository.Id, "m-zuber-octokit-integration-tests");

                var isCollab = await fixture.IsCollaborator(context.Repository.Id, "m-zuber-octokit-integration-tests");

                Assert.True(isCollab);
            }
        }
    }

    public class TheDeleteMethod
    {
        [IntegrationTest]
        public async Task CheckDeleteMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add a collaborator
                await fixture.Add(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                // and remove
                await fixture.Delete(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                var isCollab = await fixture.IsCollaborator(context.RepositoryOwner, context.RepositoryName, "m-zuber-octokit-integration-tests");

                Assert.False(isCollab);
            }
        }

        [IntegrationTest]
        public async Task CheckDeleteMethodWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                // add a collaborator
                await fixture.Add(context.Repository.Id, "m-zuber-octokit-integration-tests");

                // and remove
                await fixture.Delete(context.Repository.Id, "m-zuber-octokit-integration-tests");

                var isCollab = await fixture.IsCollaborator(context.Repository.Id, "m-zuber-octokit-integration-tests");

                Assert.False(isCollab);
            }
        }
    }

    public class TheInviteMethod
    {
        [IntegrationTest]
        public async Task CanInviteNewCollaborator()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator
                var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, "octokat", permission);

                Assert.Equal("octokat", response.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response.Permissions);
            }
        }
    }
}