using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableMarkdownClientTests
    {
        public class TheRenderArbitraryMarkdownMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMarkdownClient(gitHubClient);

                client.RenderArbitraryMarkdown(new NewArbitraryMarkdown("# test"));

                gitHubClient.Markdown.Received(1).RenderArbitraryMarkdown(Arg.Is<NewArbitraryMarkdown>(a => a.Text == "# test"));
            }
        }

        public class TheRenderRawMarkdownMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMarkdownClient(gitHubClient);

                client.RenderRawMarkdown("# test");

                gitHubClient.Markdown.Received(1).RenderRawMarkdown("# test");
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableMarkdownClient((IGitHubClient)null));
            }
        }
    }
}
