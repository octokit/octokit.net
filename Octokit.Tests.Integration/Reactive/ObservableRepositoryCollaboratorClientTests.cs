using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class ObservableRepositoryCollaboratorClientTests
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
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName).ToList();

                Assert.NotNull(collaborators);
                Assert.Single(collaborators);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithoutStart()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, options).ToList();

                Assert.NotNull(collaborators);
                Assert.Single(collaborators);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithStart()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, options).ToList();

                Assert.NotNull(collaborators);
                Assert.Empty(collaborators);
            }
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, skipStartOptions).ToList();

                Assert.Single(firstPage);
                Assert.Empty(secondPage);
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
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var isCollab = await fixture.IsCollaborator(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner);

                Assert.True(isCollab);
            }
        }
    }
    public class TheReviewPermissionMethod
    {
        [IntegrationTest]
        public async Task ReturnsReadPermissionForNonCollaborator()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var permission = await fixture.ReviewPermission(context.RepositoryOwner, context.RepositoryName, "octokitnet-test1");

                Assert.Equal("read", permission.Permission);
            }
        }

        [IntegrationTest]
        public async Task ReturnsReadPermissionForNonCollaboratorWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var permission = await fixture.ReviewPermission(context.RepositoryId, "octokitnet-test1");

                Assert.Equal("read", permission.Permission);
            }
        }

        [IntegrationTest]
        public async Task ReturnsWritePermissionForCollaboratorInvitation()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                // add a collaborator
                var invitation = await fixture.Add(context.RepositoryOwner, context.RepositoryName, "octokitnet-test1", new CollaboratorRequest("write"));

                Assert.Equal(InvitationPermissionType.Write, invitation.Permissions);
            }
        }

        [IntegrationTest]
        public async Task ReturnsWritePermissionForCollaboratorInvitationWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                // add a collaborator
                var invitation = await fixture.Add(context.RepositoryOwner, context.RepositoryName, "octokitnet-test1", new CollaboratorRequest("write"));

                Assert.Equal(InvitationPermissionType.Write, invitation.Permissions);
            }
        }

        [IntegrationTest]
        public async Task ReturnsAdminPermissionForOwner()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var permission = await fixture.ReviewPermission(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner);

                Assert.Equal("admin", permission.Permission);
            }
        }

        [IntegrationTest]
        public async Task ReturnsAdminPermissionForOwnerWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var permission = await fixture.ReviewPermission(context.RepositoryId, context.RepositoryOwner);

                Assert.Equal("admin", permission.Permission);
            }
        }

        [PaidAccountTest]
        public async Task ReturnsNonePermissionForPrivateRepository()
        {
            var github = Helper.GetAuthenticatedClient();
            var userDetails = await github.User.Current();
            if (userDetails.Plan.PrivateRepos == 0)
            {
                throw new Exception("Test cannot complete, account is on free plan");
            }

            var repoName = Helper.MakeNameWithTimestamp("private-repo");
            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { Private = true }))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var permission = await fixture.ReviewPermission(context.RepositoryOwner, context.RepositoryName, "octokitnet-test1");

                Assert.Equal("none", permission.Permission);
            }
        }

        [PaidAccountTest]
        public async Task ReturnsNonePermissionForPrivateRepositoryWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var userDetails = await github.User.Current();
            if (userDetails.Plan.PrivateRepos == 0)
            {
                throw new Exception("Test cannot complete, account is on free plan");
            }

            var repoName = Helper.MakeNameWithTimestamp("private-repo");
            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { Private = true }))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);

                var permission = await fixture.ReviewPermission(context.RepositoryId, "octokitnet-test1");

                Assert.Equal("none", permission.Permission);
            }
        }
    }
}