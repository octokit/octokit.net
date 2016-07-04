using NSubstitute;
using Octokit;
using System;
using System.Threading.Tasks;
using Xunit;

public class RepositoryInvitationsClientTests
{
    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInvitationsClient(null));
        }
    }

    public class TheGetAllForRepositoryMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryInvitationsClient(connection);

            await client.GetAllForRepository(1);

            connection.Received().GetAll<RepositoryInvitation>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/invitations"), "application/vnd.github.swamp-thing-preview+json");
        }
    }

    public class TheGetAllForCurrentMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryInvitationsClient(connection);

            await client.GetAllForCurrent();

            connection.Received().GetAll<RepositoryInvitation>(Arg.Is<Uri>(u => u.ToString() == "user/repository_invitations"), "application/vnd.github.swamp-thing-preview+json");
        }
    }

    public class TheAcceptMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryInvitationsClient(connection);

            await client.Accept(1);

            connection.Connection.Received().Patch(Arg.Is<Uri>(u => u.ToString() == "user/repository_invitations/1"), "application/vnd.github.swamp-thing-preview+json");
        }
    }

    public class TheDeclineMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryInvitationsClient(connection);

            await client.Decline(1);

            connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "user/repository_invitations/1"), Arg.Any<object>(), "application/vnd.github.swamp-thing-preview+json");
        }
    }

    public class TheDeleteMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryInvitationsClient(connection);

            await client.Delete(1, 2);

            connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/invitations/2"), Arg.Any<object>(), "application/vnd.github.swamp-thing-preview+json");
        }
    }

    public class TheEditMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryInvitationsClient(connection);
            var updatedInvitation = new InvitationUpdate(InvitationPermissionType.Read);

            await client.Edit(1, 2, updatedInvitation);

            connection.Received().Patch<RepositoryInvitation>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/invitations/2"), Arg.Is<InvitationUpdate>(updatedInvitation), "application/vnd.github.swamp-thing-preview+json");
        }

        [Fact]
        public async Task EnsureNonNullArguments()
        {
            var client = new RepositoryInvitationsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Edit(1, 2, null));
        }
    }

}

