using System;
using NSubstitute;
using Octokit;
using Xunit;

public class GitDatabaseClientTests
{
    public class TheCtor
    {
        [Fact]
        public void EnsuresArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new GitDatabaseClient(null));
        }

        [Fact]
        public void SetChildsClients()
        {
            var apiConnection = Substitute.For<IApiConnection>();
            var gitDatabaseClient = new GitDatabaseClient(apiConnection);
            Assert.NotNull(gitDatabaseClient.Tag);
        }
    }    
}