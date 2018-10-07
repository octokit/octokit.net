using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableUserKeysClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserKeysClient(gitHubClient);

                client.GetAllForCurrent();

                gitHubClient.User.GitSshKey.Received().GetAllForCurrent(Arg.Any<ApiOptions>());
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

                gitHubClient.User.GitSshKey.Received().GetAll("auser", Arg.Any<ApiOptions>());
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

                gitHubClient.User.GitSshKey.Received().Get(1);
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

                gitHubClient.User.GitSshKey.Received().Create(
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

                gitHubClient.User.GitSshKey.Received().Delete(1);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableUserKeysClient(null));
            }
        }
    }
}
