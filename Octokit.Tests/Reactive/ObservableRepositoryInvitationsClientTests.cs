using NSubstitute;
using Octokit.Reactive;
using System;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryInvitationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryInvitationsClient(null));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(42, null));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);

                client.GetAllForRepository(42);

                gitHub.Received().Repository.Invitation.GetAllForRepository(42);
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent(null));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);

                client.GetAllForCurrent();

                gitHub.Received().Repository.Invitation.GetAllForCurrent();
            }
        }

        public class TheAcceptMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);

                client.Accept(42);

                gitHub.Received().Repository.Invitation.Accept(42);
            }
        }

        public class TheDeclineMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);

                client.Decline(42);

                gitHub.Received().Repository.Invitation.Decline(42);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);

                client.Delete(42, 43);

                gitHub.Received().Repository.Invitation.Delete(42, 43);
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);
                var update = new InvitationUpdate(InvitationPermissionType.Write);

                client.Edit(42, 43, update);

                gitHub.Received().Repository.Invitation.Edit(42, 43, update);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var gitHub = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryInvitationsClient(gitHub);

                Assert.Throws<ArgumentNullException>(() => client.Edit(1, 2, null));
            }
        }
    }
}