using NSubstitute;
using Octokit;
using System;
using System.Threading.Tasks;
using Xunit;

public class RepositoryInvitationsClientTests
{
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

}

