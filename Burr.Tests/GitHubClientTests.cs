using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Burr.Tests
{
    public class GitHubClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CanCreateAnonymousClient()
            {
                var client = new GitHubClient();

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [Fact]
            public void CanCreateBasicAuthClient()
            {
                var client = new GitHubClient { Username = "tclem", Password = "pwd" };

                client.AuthenticationType.Should().Be(AuthenticationType.Basic);
            }

            [Fact]
            public void CanCreateOauthClient()
            {
                var client = new GitHubClient { Token = "abiawethoasdnoi" };

                client.AuthenticationType.Should().Be(AuthenticationType.Oauth);
            }
        }
    }
}
