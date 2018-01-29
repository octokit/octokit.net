using System;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class AccessTokensClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new AccessTokensClient(null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AccessTokensClient(connection);

                int fakeInstallationId = 3141;

                client.Create(fakeInstallationId);

                connection.Received().Post<AccessToken>(Arg.Is<Uri>(u => u.ToString() == "installations/3141/access_tokens"), string.Empty, AcceptHeaders.MachineManPreview);
            }
        }
    }
}
