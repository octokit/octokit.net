using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservablePublicKeysClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePublicKeysClient(gitHubClient);

                client.Get(PublicKeyType.SecretScanning);

                gitHubClient.Meta.PublicKeys.Received(1).Get(PublicKeyType.SecretScanning);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservablePublicKeysClient((IGitHubClient)null));
            }
        }
    }
}
