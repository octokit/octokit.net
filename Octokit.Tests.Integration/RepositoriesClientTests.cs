﻿using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class RepositoriesClientTests
    {
        public class TheGetAsyncMethod
        {
            [IntegrationTest]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var repository = await github.Repository.Get("ReactiveCocoa", "ReactiveCocoa");

                repository.CloneUrl.Should().Be("https://github.com/ReactiveCocoa/ReactiveCocoa.git");
            }
        }

        public class TheGetAllForOrgMethod
        {
            [IntegrationTest]
            public async Task ReturnsAllRepositoriesForOrganization()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var repositories = await github.Repository.GetAllForOrg("github");

                repositories.Count.Should().BeGreaterThan(80);
            }
        }

        public class TheGetReadmeMethod
        {
            [IntegrationTest]
            public async Task ReturnsReadmeForOctokit()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                // TODO: Change this to request github/Octokit.net once we make this OSS.
                var readme = await github.Repository.GetReadme("haacked", "seegit");
                readme.Name.Should().Be("README.md");
                var readMeHtml = await readme.GetHtmlContent();
                readMeHtml.Should().Contain(@"<div id=""readme""");
                readMeHtml.Should().Contain("<p><strong>WARNING: This is some haacky code.");
            }
        }
    }
}
