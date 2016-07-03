using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableUserGpgKeysClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserGpgKeysClient(githubClient);

                client.GetAllForCurrent();

                githubClient.User.GpgKey.Received().GetAllForCurrent(Arg.Any<ApiOptions>());
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserGpgKeysClient(githubClient);

                client.Get(1);

                githubClient.User.GpgKey.Received().Get(1);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserGpgKeysClient(githubClient);

                client.Create(new NewGpgKey("ABCDEFG"));

                githubClient.User.GpgKey.Received().Create(Arg.Is<NewGpgKey>(k => k.ArmoredPublicKey == "ABCDEFG"));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableUserGpgKeysClient(githubClient);

                client.Delete(1);

                githubClient.User.GpgKey.Received().Delete(1);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableUserGpgKeysClient(null));
            }
        }
    }
}
