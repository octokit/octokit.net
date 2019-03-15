using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class ObservableRepositoryCollaboratorClientTests
{
    static async Task AcceptInvitation(IGitHubClient github, IGitHubClient github2, long repositoryId, string user)
    {
        var invitations = await github.Repository.Invitation.GetAllForRepository(repositoryId);
        await github2.Repository.Invitation.Accept(invitations.First(i => i.Invitee.Login == user).Id);        
    }

    public class TheGetAllMethod
    {
        readonly IGitHubClient github;
        readonly IObservableRepoCollaboratorsClient client;

        readonly IGitHubClient github2;

        public TheGetAllMethod()
        {
            github = Helper.GetAuthenticatedClient();
            client = new ObservableRepoCollaboratorsClient(github);

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

                var collaborators = await client.GetAll(context.RepositoryOwner, context.RepositoryName).ToList();
                Assert.NotNull(collaborators);
                Assert.Equal(2, collaborators.Count);
                Assert.NotNull(collaborators[0].Permissions);
                Assert.NotNull(collaborators[1].Permissions);
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

                var collaborators = await client.GetAll(context.RepositoryOwner, context.RepositoryName, new ListCollaboratorRequest(), options).ToList();
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

                var collaborators = await client.GetAll(context.RepositoryOwner, context.RepositoryName, new ListCollaboratorRequest(), options).ToList();
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

                var firstPage = await client.GetAll(context.RepositoryOwner, context.RepositoryName, new ListCollaboratorRequest(), startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await client.GetAll(context.RepositoryOwner, context.RepositoryName, new ListCollaboratorRequest(), skipStartOptions).ToList();

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }
    }

    public class TheIsCollaboratorMethod
    {
        readonly IGitHubClient github;
        readonly IObservableRepoCollaboratorsClient client;

        readonly IGitHubClient github2;

        public TheIsCollaboratorMethod()
        {
            github = Helper.GetAuthenticatedClient();
            client = new ObservableRepoCollaboratorsClient(github);

            github2 = Helper.GetAuthenticatedClient(true);
        }

        [DualAccountTest]
        public async Task ReturnsTrueIfUserIsCollaborator()
        {
            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext("public-repo"))
            {
                // add a collaborator
                client.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var isCollab = await client.IsCollaborator(context.RepositoryOwner, context.RepositoryName, collaborator);

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

                Assert.Equal(PermissionLevel.Read, permission.Permission);
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

                Assert.Equal(PermissionLevel.Read, permission.Permission);
            }
        }

        [DualAccountTest]
        public async Task ReturnsWritePermissionForCollaborator()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            var github2 = Helper.GetAuthenticatedClient(true);

            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);
                
                // add a collaborator
                await fixture.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var permission = await fixture.ReviewPermission(context.RepositoryOwner, context.RepositoryName, collaborator);

                Assert.Equal(PermissionLevel.Write, permission.Permission);
            }
        }

        [DualAccountTest]
        public async Task ReturnsWritePermissionForCollaboratorWithRepositoryId()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            var github2 = Helper.GetAuthenticatedClient(true);

            var collaborator = Helper.UserName2;
            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = new ObservableRepoCollaboratorsClient(github);
                
                // add a collaborator
                await fixture.Add(context.RepositoryOwner, context.RepositoryName, collaborator);
                await AcceptInvitation(github, github2, context.RepositoryId, collaborator);

                var permission = await fixture.ReviewPermission(context.RepositoryId, collaborator);

                Assert.Equal(PermissionLevel.Write, permission.Permission);
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

                Assert.Equal(PermissionLevel.Admin, permission.Permission);
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

                Assert.Equal(PermissionLevel.Admin, permission.Permission);
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

                Assert.Equal(PermissionLevel.None, permission.Permission);
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

                Assert.Equal(PermissionLevel.None, permission.Permission);
            }
        }
    }
}