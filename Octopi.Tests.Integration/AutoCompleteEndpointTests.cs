using System.Threading.Tasks;
using FluentAssertions;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests.Integration
{
    public class AutoCompleteEndpointTests
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
