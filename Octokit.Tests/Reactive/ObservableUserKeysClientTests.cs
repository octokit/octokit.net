using System;
using System.Linq;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableUserKeysClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserKeysClient(gitHubClient);

                client.GetAllForCurrent();

                gitHubClient.User.Keys.Received().GetAllForCurrent();
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserKeysClient(gitHubClient);

                client.GetAll("auser");

                gitHubClient.User.Keys.Received().GetAll("auser");
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserKeysClient(gitHubClient);

                client.Get(1);

                gitHubClient.User.Keys.Received().Get(1);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserKeysClient(gitHubClient);

                client.Create(new NewPublicKey("title", "ABCDEFG"));

                gitHubClient.User.Keys.Received().Create(
                    Arg.Is<NewPublicKey>(a =>
                        a.Title == "title" &&
                        a.Key == "ABCDEFG"));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserKeysClient(gitHubClient);

                client.Delete(1);

                gitHubClient.User.Keys.Received().Delete(1);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableUserKeysClient(null));
            }
        }
    }
}
