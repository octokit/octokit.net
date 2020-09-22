using System;
using NSubstitute;
using Octokit;
using Xunit;

public class ActionsClientTests
{
    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new ActionsClient(null));
        }

        [Fact]
        public void SetWorkflowsClient()
        {
            var apiConnection = Substitute.For<IApiConnection>();
            var actionsClient = new ActionsClient(apiConnection);
            Assert.NotNull(actionsClient.Workflows);
        }
    }
}
