using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableEnterpriseLicenseClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseLicenseClient(github);

                client.Get();
                github.Enterprise.License.Received(1).Get();
            }
        }
    }
}
