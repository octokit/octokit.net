﻿using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class AutoCompleteClientTests
    {
        public class TheGetEmojisMethod
        {
            [IntegrationTest]
            public async Task GetsAllTheEmojis()
            {
                var github = new GitHubClient
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var emojis = await github.AutoComplete.GetEmojis();

                emojis.Count.Should().BeGreaterThan(1);
            }
        }
    }
}
