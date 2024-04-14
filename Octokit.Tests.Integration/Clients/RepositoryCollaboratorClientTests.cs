using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

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

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName);

                Assert.NotNull(collaborators);
                Assert.Single(collaborators);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCollaboratorsWithPermissionFilter()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;

                var collaborators = await fixture.GetAll(context.RepositoryOwner,
                                                         context.RepositoryName,
                                                         new RepositoryCollaboratorListRequest() { Permission = CollaboratorPermission.Admin });

                Assert.NotNull(collaborators);
                Assert.Single(collaborators);
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

                var collaborators = await fixture.GetAll(context.Repository.Id);

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
                var fixture = github.Repository.Collaborator;

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, options);

                Assert.NotNull(collaborators);
                Assert.Single(collaborators);
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

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var collaborators = await fixture.GetAll(context.Repository.Id, options);

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
                var fixture = github.Repository.Collaborator;

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var collaborators = await fixture.GetAll(context.RepositoryOwner, context.RepositoryName, options);

                Assert.NotNull(collaborators);
                Assert.Empty(collaborators);
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

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var collaborators = await fixture.GetAll(context.Repository.Id, options);

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
                var fixture = github.Repository.Collaborator;

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

                Assert.Single(firstPage);
                Assert.Empty(secondPage);
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
                var fixture = github.Repository.Collaborator;

                var isCollab = await fixture.IsCollaborator(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner);

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

                var isCollab = await fixture.IsCollaborator(context.Repository.Id, context.RepositoryOwner);

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
                var fixture = github.Repository.Collaborator;

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
                var fixture = github.Repository.Collaborator;

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
                var fixture = github.Repository.Collaborator;

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
                var fixture = github.Repository.Collaborator;

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
                var fixture = github.Repository.Collaborator;

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
                var fixture = github.Repository.Collaborator;

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
                var fixture = github.Repository.Collaborator;

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
                var fixture = github.Repository.Collaborator;

                var permission = await fixture.ReviewPermission(context.RepositoryId, "octokitnet-test1");

                Assert.Equal("none", permission.Permission);
            }
        }
    }

    public class TheDeleteMethod
    {
        [Fact(Skip = "Adding a collaborator sends an invitation, need to figure out a way to test the remove method.")]
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

        [Fact(Skip = "Adding a collaborator sends an invitation, need to figure out a way to test the remove method.")]
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
                var permission = new CollaboratorRequest("push");

                // invite a collaborator
                var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, "octokat", permission);

                Assert.Equal("octokat", response.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response.Permissions);
            }
        }
    }
}