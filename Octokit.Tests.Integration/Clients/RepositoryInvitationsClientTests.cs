using System.Collections.Generic;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class RepositoryInvitationsClientTests
{
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
                var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner, permission);

                Assert.Equal(context.RepositoryOwner, response.Invitee.Login);
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

        [DualAccountTest]
        public async Task ReturnsCorrectCountOfInvitationsWithStart()
        {
            var collaborator1 = Helper.CredentialsSecondUser.Login;
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator
                var response1 = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, collaborator1, permission);

                Assert.Equal(collaborator1, response1.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response1.Permissions);

                var response2 = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner, permission);

                Assert.Equal(context.RepositoryOwner, response2.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response2.Permissions);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var invitations = await github.Repository.Invitation.GetAllForRepository(context.Repository.Id, options);

                Assert.Equal(1, invitations.Count);
            }
        }

        [DualAccountTest]
        public async Task ReturnsCorrectCountOfInvitationsWithoutStart()
        {
            var collaborator = Helper.CredentialsSecondUser.Login;
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator
                var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, collaborator, permission);

                Assert.Equal(collaborator, response.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response.Permissions);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                };

                var invitations = await github.Repository.Invitation.GetAllForRepository(context.Repository.Id, options);

                Assert.Equal(1, invitations.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsDistinctInvitationsBasedOnStart()
        {
            var collaborator1 = Helper.CredentialsSecondUser.Login;
            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            using (var context = await github.CreateRepositoryContext(new NewRepository(repoName)))
            {
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator
                var response1 = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, collaborator1, permission);

                Assert.Equal(collaborator1, response1.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response1.Permissions);

                var response2 = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner, permission);

                Assert.Equal(context.RepositoryOwner, response2.Invitee.Login);
                Assert.Equal(InvitationPermissionType.Write, response2.Permissions);

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var firstInvitations = await github.Repository.Invitation.GetAllForRepository(context.Repository.Id, startOptions);
                var secondInvitations = await github.Repository.Invitation.GetAllForRepository(context.Repository.Id, skipStartOptions);

                Assert.NotEqual(firstInvitations[0].Invitee.Login, secondInvitations[0].Invitee.Login);
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

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfInvitationsWithStart()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoNames = Enumerable.Range(0, 2).Select(i => Helper.MakeNameWithTimestamp($"public-repo{i}")).ToList();

            var contexts = new List<RepositoryContext>();
            try
            {
                foreach (var repoName in repoNames)
                {
                    contexts.Add(await github.CreateRepositoryContext(new NewRepository(repoName)));
                }
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator to all repos
                foreach (var context in contexts)
                {
                    var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner, permission);

                    Assert.Equal(context.RepositoryOwner, response.Invitee.Login);
                    Assert.Equal(InvitationPermissionType.Write, response.Permissions);
                }

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };


                var invitations = await github.Repository.Invitation.GetAllForCurrent(startOptions);
                Assert.Equal(1, invitations.Count);
            }
            finally
            {
                if (contexts != null)
                {
                    foreach (var context in contexts)
                    {
                        context?.Dispose();
                    }
                }
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfInvitationsWithoutStart()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoNames = Enumerable.Range(0, 2).Select(i => Helper.MakeNameWithTimestamp($"public-repo{i}")).ToList();

            var contexts = new List<RepositoryContext>();
            try
            {
                foreach (var repoName in repoNames)
                {
                    contexts.Add(await github.CreateRepositoryContext(new NewRepository(repoName)));
                }
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator to all repos
                foreach (var context in contexts)
                {
                    var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner, permission);

                    Assert.Equal(context.RepositoryOwner, response.Invitee.Login);
                    Assert.Equal(InvitationPermissionType.Write, response.Permissions);
                }

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };


                var invitations = await github.Repository.Invitation.GetAllForCurrent(startOptions);
                Assert.Equal(1, invitations.Count);
            }
            finally
            {
                if (contexts != null)
                {
                    foreach (var context in contexts)
                    {
                        context?.Dispose();
                    }
                }
            }
        }

        [IntegrationTest]
        public async Task ReturnsDistinctInvitationsBasedOnStart()
        {
            var github = Helper.GetAuthenticatedClient();
            var repoNames = Enumerable.Range(0, 2).Select(i => Helper.MakeNameWithTimestamp($"public-repo{i}")).ToList();

            var contexts = new List<RepositoryContext>();
            try
            {
                foreach (var repoName in repoNames)
                {
                    contexts.Add(await github.CreateRepositoryContext(new NewRepository(repoName)));
                }
                var fixture = github.Repository.Collaborator;
                var permission = new CollaboratorRequest(Permission.Push);

                // invite a collaborator to all repos
                foreach (var context in contexts)
                {
                    var response = await fixture.Invite(context.RepositoryOwner, context.RepositoryName, context.RepositoryOwner, permission);

                    Assert.Equal(context.RepositoryOwner, response.Invitee.Login);
                    Assert.Equal(InvitationPermissionType.Write, response.Permissions);
                }
               
                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var firstInvitations = await github.Repository.Invitation.GetAllForCurrent(startOptions);
                var secondInvitations = await github.Repository.Invitation.GetAllForCurrent(skipStartOptions);

                var invitations = firstInvitations.Concat(secondInvitations).ToArray();
                var invitationsLength = invitations.Length;
                for (int i = 0; i < invitationsLength; i++)
                {
                    for (int j = i+1; j < invitationsLength; j++)
                    {
                        Assert.NotEqual(invitations[i].Repository.FullName, invitations[j].Repository.FullName);
                    }
                }
            }
            finally
            {
                if(contexts != null)
                {
                    foreach (var context in contexts)
                    {
                        context?.Dispose();
                    }
                }
            }
        }
    }

    public class TheAcceptMethod
    {
        [IntegrationTest]
        public async Task CanAcceptInvitation()
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

                // Accept the invitation
                var accepted = await github.Repository.Invitation.Accept(response.Id);

                Assert.True(accepted);
            }
        }
    }

    public class TheDeclineMethod
    {
        [IntegrationTest]
        public async Task CanDeclineInvitation()
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

                // Decline the invitation
                var declined = await github.Repository.Invitation.Decline(response.Id);

                Assert.True(declined);
            }
        }
    }

    public class TheDeleteMethod
    {
        [IntegrationTest]
        public async Task CanDeleteInvitation()
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

                var delete = await github.Repository.Invitation.Delete(response.Repository.Id, response.Id);

                Assert.True(delete);
            }
        }
    }

    public class TheUpdateMethod
    {
        [IntegrationTest]
        public async Task CanUpdateInvitation()
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

                var updatedInvitation = new InvitationUpdate(InvitationPermissionType.Admin);

                var update = await github.Repository.Invitation.Edit(response.Repository.Id, response.Id, updatedInvitation);

                Assert.Equal(updatedInvitation.Permissions, update.Permissions);
                Assert.Equal(response.Id, update.Id);
                Assert.Equal(response.Repository.Id, update.Repository.Id);
                Assert.Equal(response.Invitee.Login, update.Invitee.Login);
                Assert.Equal(response.Inviter.Login, update.Inviter.Login);
            }
        }
    }
}

