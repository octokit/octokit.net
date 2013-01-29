using System.Threading.Tasks;
using FluentAssertions;
using Octokit.Http;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class AutoCompleteClientTests
    {
        public class TheGetEmojisMethod
        {
            [Fact]
            public async Task GetsAllTheEmojis()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var emojis = await github.AutoComplete.GetEmojis();

                emojis.Count.Should().BeGreaterThan(1);
            }
        }
    }
}
