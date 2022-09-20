using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableLicensesClientTests
    {
        public class TheGetAllLicensesMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableLicensesClient(gitHubClient);

                client.GetAllLicenses();

                gitHubClient.Licenses.Received(1).GetAllLicenses(Args.ApiOptions);
            }
        }

        public class TheGetLicenseMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableLicensesClient(gitHubClient);

                client.GetLicense("key");

                gitHubClient.Licenses.Received(1).GetLicense("key");
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableLicensesClient((IGitHubClient)null));
            }
        }
    }
}
