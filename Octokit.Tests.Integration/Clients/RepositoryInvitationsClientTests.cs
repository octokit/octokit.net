using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
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
                var permission = new CollaboratorRequest();

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
}

