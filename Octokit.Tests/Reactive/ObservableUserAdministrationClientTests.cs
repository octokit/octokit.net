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
                var administrationClient = Substitute.For<IUserAdministrationClient>();
                var client = new ObservableUserAdministrationClient(administrationClient);

                client.Promote("auser");

                administrationClient.Received().Promote("auser");
            }
        }

        public class TheDemoteMethod
        {
            [Fact]
            public void GetsFromClientDemoteDemote()
            {
                var administrationClient = Substitute.For<IUserAdministrationClient>();
                var client = new ObservableUserAdministrationClient(administrationClient);

                client.Demote("auser");

                administrationClient.Received().Demote("auser");
            }
        }

        public class TheSuspendMethod
        {
            [Fact]
            public void GetsFromClientSuspendSuspend()
            {
                var administrationClient = Substitute.For<IUserAdministrationClient>();
                var client = new ObservableUserAdministrationClient(administrationClient);

                client.Suspend("auser");

                administrationClient.Received().Suspend("auser");
            }
        }

        public class TheUnsuspendMethod
        {
            [Fact]
            public void GetsFromClientUnsuspendUnsuspend()
            {
                var administrationClient = Substitute.For<IUserAdministrationClient>();
                var client = new ObservableUserAdministrationClient(administrationClient);

                client.Unsuspend("auser");

                administrationClient.Received().Unsuspend("auser");
            }
        }
    }
}
