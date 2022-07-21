﻿using System.Reflection;
using Xunit;

namespace Octokit.Tests.Integration
{
    /// <summary>
    /// Tests to make sure our tests are ok.
    /// </summary>
    public class SelfTests
    {
        [Fact]
        public void NoTestsUseAsyncVoid()
        {
            var errors = typeof(SelfTests).GetTypeInfo().Assembly.GetAsyncVoidMethodsList();
            Assert.Equal("", errors);
        }
    }

    public class GitHubClientTestBase
    {
        protected readonly IGitHubClient _github;

        public GitHubClientTestBase()
        {
            _github = Helper.GetAuthenticatedClient();
        }
    }
}
