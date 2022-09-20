using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableGitIgnoreClientTests
    {
        public class TheGetAllGitIgnoreTemplatesMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitIgnoreClient(gitHubClient);

                client.GetAllGitIgnoreTemplates();

                gitHubClient.GitIgnore.Received(1).GetAllGitIgnoreTemplates();
            }
        }

        public class TheGetGitIgnoreTemplate
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitIgnoreClient(gitHubClient);

                client.GetGitIgnoreTemplate("template");

                gitHubClient.GitIgnore.Received(1).GetGitIgnoreTemplate("template");
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableGitIgnoreClient((IGitHubClient)null));
            }
        }
    }
}
