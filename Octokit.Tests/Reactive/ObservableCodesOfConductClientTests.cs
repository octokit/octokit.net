using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableCodesOfConductClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableCodesOfConductClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCodesOfConductClient(gitHubClient);

                client.GetAll();

                gitHubClient.CodesOfConduct.Received(1).GetAll();
            }
        }

        public class TheGetByKeyMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCodesOfConductClient(gitHubClient);

                client.Get(CodeOfConductType.ContributorCovenant);

                gitHubClient.CodesOfConduct.Received(1).Get(CodeOfConductType.ContributorCovenant);
            }
        }

        public class TheGetByRepositoryMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCodesOfConductClient(gitHubClient);

                client.Get("octokit", "octokit.net");

                gitHubClient.CodesOfConduct.Received(1).Get("octokit", "octokit.net");
            }
        }
    }
}
