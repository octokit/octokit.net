using System;
using System.Linq;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableUserAdministrationClientTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.Create(new NewUser("auser", "email@company.com"));

                gitHubClient.User.Administration.Received().Create(
                    Arg.Is<NewUser>(a =>
                        a.Login == "auser" &&
                        a.Email == "email@company.com"));
            }
        }

        public class TheRenameMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.Rename("auser", new UserRename("renamed-user"));

                gitHubClient.User.Administration.Received().Rename(
                    "auser",
                    Arg.Is<UserRename>(a =>
                        a.Login == "renamed-user"));
            }
        }

        public class TheCreateImpersonationTokenMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.CreateImpersonationToken("auser", new NewImpersonationToken(new[] { "public_repo" }));

                gitHubClient.User.Administration.Received().CreateImpersonationToken(
                    "auser",
                    Arg.Is<NewImpersonationToken>(a =>
                        a.Scopes.ToList()[0] == "public_repo"));
            }
        }

        public class TheDeleteImpersonationTokenMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.DeleteImpersonationToken("auser");

                gitHubClient.User.Administration.Received().DeleteImpersonationToken("auser");
            }
        }

        public class ThePromoteMethod
        {
            [Fact]
            public void CallsIntoClient()
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
            public void CallsIntoClient()
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
            public void CallsIntoClient()
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
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.Unsuspend("auser");

                gitHubClient.User.Administration.Received().Unsuspend("auser");
            }
        }

        public class TheListAllPublicKeysMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                var expectedUri = "admin/keys";

                client.ListAllPublicKeys();

                gitHubClient.Connection.Received().Get<System.Collections.Generic.List<PublicKey>>(
                    Arg.Is<Uri>(a =>
                        a.ToString() == expectedUri),
                    null);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.Delete("auser");

                gitHubClient.User.Administration.Received().Delete("auser");
            }
        }

        public class TheDeletePublicKeyMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserAdministrationClient(gitHubClient);

                client.DeletePublicKey(1);

                gitHubClient.User.Administration.Received().DeletePublicKey(1);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableUserAdministrationClient(null));
            }
        }
    }
}
