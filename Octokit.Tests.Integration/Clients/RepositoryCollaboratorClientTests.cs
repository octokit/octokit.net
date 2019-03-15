using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Clients;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class RepositoryCollaboratorClientTests
{
    static async Task AcceptInvitation(IGitHubClient github, IGitHubClient github2, long repositoryId, string user)
    {
        var invitations = await github.Repository.Invitation.GetAllForRepository(repositoryId);
        await github2.Repository.Invitation.Accept(invitations.First(i => i.Invitee.Login == user).Id);        
    }
    
    public class TheGetAllMethod
    {
        readonly IGitHubClient github;
        readonly IRepoCollaboratorsClient client;

        readonly IGitHubClient github2;

        public TheGetAllMethod()
        {
            github = Helper.GetAuthenticatedClient();
            client = github.Repository.Collaborator;

            github2 = Helper.GetAuthenticatedClient(true);
        }
        
        [DualAccountTest]
        public async Task ReturnsAllCollaborators()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var collaborators = await client.GetAll(context.RepositoryOwner, context.RepositoryName);
                Assert.NotNull(collaborators);
                Assert.Equal(2, collaborators.Count);
                Assert.NotNull(collaborators[0].Permissions);
                Assert.NotNull(collaborators[1].Permissions);
            }
        }
        
        [DualAccountTest]
        public async Task ReturnsAllCollaboratorsWithRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                await client.Add(context.Repository.Id, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                var collaborators = await client.GetAll(context.Repository.Id);
                Assert.NotNull(collaborators);
                Assert.Equal(2, collaborators.Count);
            }
        }

        [DualAccountTest]
        public async Task ReturnsOutsideCollaborators()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext(Helper.Organization, new NewRepository("public-repo")))
            {
                // add a collaborator
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var listCollaboratorRequest = new ListCollaboratorRequest
                {
                    Affiliation = Affiliation.Outside
                };

                var collaborators = await client.GetAll(context.RepositoryOwner, context.RepositoryName, listCollaboratorRequest);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
                Assert.Equal(collaborator, collaborators[0].Login);
            }
        }

        [DualAccountTest]
        public async Task ReturnsOutsideCollaboratorsWithRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext(Helper.Organization, new NewRepository("public-repo")))
            {
                // add a collaborator
                await client.Add(context.Repository.Id, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                var listCollaboratorRequest = new ListCollaboratorRequest
                {
                    Affiliation = Affiliation.Outside
                };

                var collaborators = await client.GetAll(context.Repository.Id, listCollaboratorRequest);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
                Assert.Equal(collaborator, collaborators[0].Login);
            }
        }

        [DualAccountTest]
        public async Task ReturnsDirectCollaborators()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext(Helper.Organization, new NewRepository("public-repo")))
            {
                // add a collaborator
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var listCollaboratorRequest = new ListCollaboratorRequest
                {
                    Affiliation = Affiliation.Direct
                };

                var collaborators = await client.GetAll(context.RepositoryOwner, context.RepositoryName, listCollaboratorRequest);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
                Assert.Equal(collaborator, collaborators[0].Login);
            }
        }

        [DualAccountTest]
        public async Task ReturnsDirectCollaboratorsWithRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext(Helper.Organization, new NewRepository("public-repo")))
            {
                // add a collaborator
                await client.Add(context.Repository.Id, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                var listCollaboratorRequest = new ListCollaboratorRequest
                {
                    Affiliation = Affiliation.Direct
                };

                var collaborators = await client.GetAll(context.Repository.Id, listCollaboratorRequest);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
                Assert.Equal(collaborator, collaborators[0].Login);
            }
        }

        [DualAccountTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithoutStart()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add some collaborators
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var collaborators = await client.GetAll(context.RepositoryOwner, context.RepositoryName, options);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
            }
        }

        [DualAccountTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithoutStartAndRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add some collaborators
                await client.Add(context.Repository.Id, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var collaborators = await client.GetAll(context.Repository.Id, options);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
            }
        }

        [DualAccountTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithStart()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add some collaborators
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var collaborators = await client.GetAll(context.RepositoryOwner, context.RepositoryName, options);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
            }
        }

        [DualAccountTest]
        public async Task ReturnsCorrectCountOfCollaboratorsWithStartAndRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add some collaborators
                await client.Add(context.Repository.Id, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var collaborators = await client.GetAll(context.Repository.Id, options);
                Assert.NotNull(collaborators);
                Assert.Equal(1, collaborators.Count);
            }
        }

        [DualAccountTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add some collaborators
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await client.GetAll(context.RepositoryOwner, context.RepositoryName, new ListCollaboratorRequest(), startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await client.GetAll(context.RepositoryOwner, context.RepositoryName, new ListCollaboratorRequest(), skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }

        [DualAccountTest]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add some collaborators
                await client.Add(context.Repository.Id, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await client.GetAll(context.Repository.Id, new ListCollaboratorRequest(), startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await client.GetAll(context.Repository.Id, new ListCollaboratorRequest(), skipStartOptions);

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }

    }

    public class TheIsCollaboratorMethod
    {
        readonly IGitHubClient github;
        readonly IRepoCollaboratorsClient client;

        readonly IGitHubClient github2;

        public TheIsCollaboratorMethod()
        {
            github = Helper.GetAuthenticatedClient();
            client = github.Repository.Collaborator;

            github2 = Helper.GetAuthenticatedClient(true);
        }
        
        [DualAccountTest]
        public async Task ReturnsTrueIfUserIsCollaborator()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var isCollab = await client.IsCollaborator(context.RepositoryOwner, context.RepositoryName, collaborator);

                Assert.True(isCollab);
            }
        }

        [DualAccountTest]
        public async Task ReturnsTrueIfUserIsCollaboratorWithRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                await client.Add(context.Repository.Id, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                var isCollab = await client.IsCollaborator(context.Repository.Id, collaborator);

                Assert.True(isCollab);
            }
        }
    }

    public class TheReviewPermissionMethod
    {
        readonly IGitHubClient github;
        readonly IRepoCollaboratorsClient client;

        readonly IGitHubClient github2;

        public TheReviewPermissionMethod()
        {
            github = Helper.GetAuthenticatedClient();
            client = github.Repository.Collaborator;

            github2 = Helper.GetAuthenticatedClient(true);
        }
        
        [DualAccountTest]
        public async Task ReturnsReadPermissionForNonCollaborator()
        {
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                var permission = await client.ReviewPermission(context.RepositoryOwner, context.RepositoryName, Helper.UserName2);

                Assert.Equal(PermissionLevel.Read, permission.Permission);
            }
        }

        [DualAccountTest]
        public async Task ReturnsReadPermissionForNonCollaboratorWithRepositoryId()
        {
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                var permission = await client.ReviewPermission(context.RepositoryId, Helper.UserName2);

                Assert.Equal(PermissionLevel.Read, permission.Permission);
            }
        }

        [DualAccountTest]
        public async Task ReturnsWritePermissionForCollaborator()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var permission = await client.ReviewPermission(context.RepositoryOwner, context.RepositoryName, collaborator);

                Assert.Equal(PermissionLevel.Write, permission.Permission);
            }
        }

        [DualAccountTest]
        public async Task ReturnsWritePermissionForCollaboratorWithRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                var permission = await client.ReviewPermission(context.RepositoryId, collaborator);

                Assert.Equal(PermissionLevel.Write, permission.Permission);
            }
        }

        [IntegrationTest]
        public async Task ReturnsAdminPermissionForOwner()
        {
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                var permission = await client.ReviewPermission(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner);

                Assert.Equal(PermissionLevel.Admin, permission.Permission);
            }
        }

        [IntegrationTest]
        public async Task ReturnsAdminPermissionForOwnerWithRepositoryId()
        {
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                var permission = await client.ReviewPermission(context.RepositoryId, context.RepositoryOwner);

                Assert.Equal(PermissionLevel.Admin, permission.Permission);
            }
        }

        [PaidAccountTest]
        public async Task ReturnsNonePermissionForPrivateRepository()
        {
            var userDetails = await github.User.Current();
            if (userDetails.Plan.PrivateRepos == 0)
            {
                throw new Exception("Test cannot complete, account is on free plan");
            }

            var repoName = Helper.MakeNameWithTimestamp("private-repo");
            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { Private = true }))
            {
                var permission = await client.ReviewPermission(context.RepositoryOwner, context.RepositoryName, "octokitnet-test1");

                Assert.Equal(PermissionLevel.None, permission.Permission);
            }
        }

        [PaidAccountTest]
        public async Task ReturnsNonePermissionForPrivateRepositoryWithRepositoryId()
        {
            var userDetails = await github.User.Current();
            if (userDetails.Plan.PrivateRepos == 0)
            {
                throw new Exception("Test cannot complete, account is on free plan");
            }

            var repoName = Helper.MakeNameWithTimestamp("private-repo");
            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName) { Private = true }))
            {
                var permission = await client.ReviewPermission(context.RepositoryId, "octokitnet-test1");

                Assert.Equal(PermissionLevel.None, permission.Permission);
            }
        }
    }

    public class TheDeleteMethod
    {
        readonly IGitHubClient github;
        readonly IRepoCollaboratorsClient client;

        readonly IGitHubClient github2;

        public TheDeleteMethod()
        {
            github = Helper.GetAuthenticatedClient();
            client = github.Repository.Collaborator;

            github2 = Helper.GetAuthenticatedClient(true);
        }
        
        [DualAccountTest]
        public async Task CheckDeleteMethod()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                await client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                // and remove
                await client.Delete(context.RepositoryOwner, context.RepositoryName, collaborator);

                var isCollab = await client.IsCollaborator(context.RepositoryOwner, context.RepositoryName, collaborator);

                Assert.False(isCollab);
            }
        }

        [DualAccountTest]
        public async Task CheckDeleteMethodWithRepositoryId()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                await client.Add(context.Repository.Id, collaborator);
                await AcceptInvitation(github, github2, context.Repository.Id, collaborator);

                // and remove
                await client.Delete(context.Repository.Id, collaborator);

                var isCollab = await client.IsCollaborator(context.Repository.Id, collaborator);

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