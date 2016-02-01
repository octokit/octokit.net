using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableUserAdministrationClientTests
    {
        public class ThePromoteMethod
        {
            [Fact]
            public void GetsFromClientPromtePromote()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.Promote("auser");

                gitHubClient.User.Administration.Received().Promote("auser");
            }
        }

        public class TheDemoteMethod
        {
            [Fact]
            public void GetsFromClientDemoteDemote()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.Demote("auser");

                gitHubClient.User.Administration.Received().Demote("auser");
            }
        }

        public class TheSuspendMethod
        {
            [Fact]
            public void GetsFromClientSuspendSuspend()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.Suspend("auser");

                gitHubClient.User.Administration.Received().Suspend("auser");
            }
        }

        public class TheUnsuspendMethod
        {
            [Fact]
            public void GetsFromClientUnsuspendUnsuspend()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.Unsuspend("auser");

                gitHubClient.User.Administration.Received().Unsuspend("auser");
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableUserAdministrationClient(null));
            }
        }
    }
}
