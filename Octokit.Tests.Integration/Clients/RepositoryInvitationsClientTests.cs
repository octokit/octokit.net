using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class RepositoryInvitationsClientTests
{
    const string owner = "octocat";
    const string name = "Hello-World";

    public class TheGetAllForRepositoryMethod
    {
        [IntegrationTest]
        public async Task CanGetAllInvitations()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator
                var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, owner, permission);

                Assert.Equal(owner, response.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response.Permissions);

                var invitations = await github.Repository.Invitation.GetAllForRepository(context.Repository.Id);

                Assert.Equal(1, invitations.Count);
                Assert.Equal(invitations[0].CreatedAt, response.CreatedAt);
                Assert.Equal(invitations[0].Id, response.Id);
                Assert.Equal(invitations[0].Invitee.Login, response.Invitee.Login);
                Assert.Equal(invitations[0].Inviter.Login, response.Inviter.Login);
                Assert.Equal(invitations[0].Permissions, response.Permissions);
                Assert.Equal(invitations[0].Repository.Id, response.Repository.Id);
            }
        }
    }

    public class TheGetAllForCurrentMethod
    {
        [IntegrationTest]
        public async Task CanGetAllInvitations()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator
                var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner, permission);

                Assert.Equal(context.RepositoryOwner, response.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response.Permissions);

                var invitations = await github.Repository.Invitation.GetAllForCurrent();

                Assert.True(invitations.Count >= 1);
                Assert.NotNull(invitations.FirstOrDefault(i => i.CreatedAt == response.CreatedAt));
                Assert.NotNull(invitations.FirstOrDefault(i => i.Id == response.Id));
                Assert.NotNull(invitations.FirstOrDefault(i => i.Inviter.Login == response.Inviter.Login));
                Assert.NotNull(invitations.FirstOrDefault(i => i.Invitee.Login == response.Invitee.Login));
                Assert.NotNull(invitations.FirstOrDefault(i => i.Permissions == response.Permissions));
                Assert.NotNull(invitations.FirstOrDefault(i => i.Repository.Id == response.Repository.Id));
            }
        }
    }
}

