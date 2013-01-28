﻿using System.Threading.Tasks;
using FluentAssertions;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests.Integration
{
    public class RepositoriesEndpointTests
    {
        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var repository = await github.Repository.Get("github", "ReactiveCocoa");

                repository.CloneUrl.Should().Be("https://github.com/github/ReactiveCocoa.git");
            }
        }

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public async Task ReturnsAllRepositoriesForOrganization()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var repositories = await github.Repository.GetAllForOrg("github");

                repositories.Count.Should().BeGreaterThan(80);
            }
        }

        public class TheGetReadmeMethod
        {
            [Fact]
            public async Task ReturnsReadmeForOctopi()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                // TODO: Change this to request github/octopi once we make this OSS.
                var readme = await github.Repository.GetReadme("haacked", "seegit");
                readme.Name.Should().Be("README.md");
                var readMeHtml = await readme.GetHtmlContent();
                readMeHtml.Should().Contain(@"<div id=""readme""");
                readMeHtml.Should().Contain("<p><strong>WARNING: This is some haacky code.");
            }
        }
    }
}
