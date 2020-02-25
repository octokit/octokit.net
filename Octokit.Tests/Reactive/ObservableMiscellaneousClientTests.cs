using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableMiscellaneousClientTests
    {
        public class TheGetAllEmojisMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.GetAllEmojis();

                gitHubClient.Miscellaneous.Received(1).GetAllEmojis();
            }
        }

        public class TheRenderArbitraryMarkdownMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.RenderArbitraryMarkdown(new NewArbitraryMarkdown("# test"));

                gitHubClient.Miscellaneous.Received(1).RenderArbitraryMarkdown(Arg.Is<NewArbitraryMarkdown>(a => a.Text == "# test"));
            }
        }

        public class TheRenderRawMarkdownMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.RenderRawMarkdown("# test");

                gitHubClient.Miscellaneous.Received(1).RenderRawMarkdown("# test");
            }
        }

        public class TheGetAllGitIgnoreTemplatesMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.GetAllGitIgnoreTemplates();

                gitHubClient.Miscellaneous.Received(1).GetAllGitIgnoreTemplates();
            }
        }

        public class TheGetGitIgnoreTemplate
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.GetGitIgnoreTemplate("template");

                gitHubClient.Miscellaneous.Received(1).GetGitIgnoreTemplate("template");
            }
        }

        public class TheGetAllLicensesMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.GetAllLicenses();

                gitHubClient.Miscellaneous.Received(1).GetAllLicenses(Args.ApiOptions);
            }
        }

        public class TheGetLicenseMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.GetLicense("key");

                gitHubClient.Miscellaneous.Received(1).GetLicense("key");
            }
        }

        public class TheGetRateLimitsMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.GetRateLimits();

                gitHubClient.Miscellaneous.Received(1).GetRateLimits();
            }
        }

        public class TheGetMetadataMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMiscellaneousClient(gitHubClient);

                client.GetMetadata();

                gitHubClient.Miscellaneous.Received(1).GetMetadata();
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableMiscellaneousClient((IGitHubClient)null));
            }
        }
    }
}
