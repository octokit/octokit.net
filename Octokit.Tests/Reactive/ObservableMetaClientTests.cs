using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableMetaClientTests
    {
        public class TheGetMetadataMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMetaClient(gitHubClient);

                client.GetMetadata();

                gitHubClient.Meta.Received(1).GetMetadata();
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableMetaClient((IGitHubClient)null));
            }
        }
    }
}
